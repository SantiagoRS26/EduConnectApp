import React, { useEffect, useState, useRef } from "react";
import SideBar from "../../components/SiderBar";
import chatController from "../../services/api/chatController";
import { useParams } from "react-router-dom";
import { Button } from "@material-tailwind/react";
import { PaperAirplaneIcon } from "@heroicons/react/24/solid";
import createSignalRConnection from '../../utils/SignalRClient';


const Chat = () => {
    const { chatId } = useParams();
    const [chatMessages, setChatMessages] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [userId, setUserId] = useState(null);
    const [messageInput, setMessageInput] = useState("");
    const textareaRef = useRef(null);
    const messagesListRef = useRef(null);
    const [connection, setConnection] = useState(null);


    useEffect(() => {
        const initializeSignalRConnection = async () => {
            const signalRConnection = createSignalRConnection();

            signalRConnection
                .start()
                .then(() => {
                    console.log("Conexión establecida con el servidor SignalR");
                })
                .catch((error) => {
                    console.error(
                        "Error al establecer la conexión con el servidor SignalR",
                        error
                    );
                });

            setConnection(signalRConnection); // Capturar la conexión en el estado
        };

        initializeSignalRConnection();
    }, []);



    useEffect(() => {
        const fetchChatMessages = async () => {
            try {
                const data = await chatController.messages(chatId);
                if (data) {
                    setIsLoading(false);
                    setChatMessages(data.Messages);
                    setUserId(data.UserId.toUpperCase());
                    scrollMessagesToBottom();
                }
            } catch (error) {
                console.log(error);
            }
        };

        fetchChatMessages();

        if (connection) {
            connection.on("ReceiveMessage", (message) => {
                setChatMessages((prevMessages) => [...prevMessages, message]);
                scrollMessagesToBottom();
            });

            return () => {
                connection.off("ReceiveMessage");
            };
        }
    }, [chatId, connection]);

    const handleMessageChange = (e) => {
        setMessageInput(e.target.value);
        adjustTextareaHeight();
    };

    const adjustTextareaHeight = () => {
        const textarea = textareaRef.current;
        textarea.style.height = "auto";
        textarea.style.height = `${textarea.scrollHeight + 4}px`;

        // Limitar a un máximo de cuatro líneas
        const maxHeight = parseInt(getComputedStyle(textarea).lineHeight) * 4;
        if (textarea.scrollHeight > maxHeight) {
            textarea.style.height = `${maxHeight}px`;
            textarea.style.overflowY = "auto";
        } else {
            textarea.style.overflowY = "hidden";
        }
    };

    const sendMessageToServer = () => {
        // Obtener el contenido del mensaje
        const content = messageInput.trim();
        if (content !== "") {
            // Verificar que la conexión esté establecida
            if (connection) {
                // Enviar el mensaje al hub del lado del servidor
                connection
                    .invoke("SendMessage", chatId, content)
                    .then(() => {
                        // Limpiar el input del mensaje
                        setMessageInput("");
                        const sentMessage = {
                            SenderId: userId,
                            Message: content,
                            SentDate: new Date().toISOString()
                        };
                        setChatMessages((prevMessages) => [...prevMessages, sentMessage]);
                        scrollMessagesToBottom();
                    })
                    .catch((error) => {
                        console.error("Error al enviar el mensaje:", error);
                    });
            } else {
                console.error("La conexión no está establecida.");
            }
        }
    };

    const handleKeyDown = (e) => {
        if (e.key === "Enter") {
            e.preventDefault();
            sendMessageToServer();
        }
    };

    useEffect(() => {
        scrollMessagesToBottom();
    }, [chatMessages]);

    const scrollMessagesToBottom = () => {
        messagesListRef.current.scrollTop = messagesListRef.current.scrollHeight;
    };

    return (
        <div className="flex">
            <div className="w-[20rem]">
                <SideBar />
            </div>
            <div className="h-screen flex-1 flex justify-center items-center bg-gradient-to-r from-blue-200 to-cyan-200">
                <div className="h-4/5 w-4/5 backdrop-blur-md bg-white/25 rounded-3xl overflow-y-auto p-16 flex flex-col">
                    <div className="w-full h-[90%] flex flex-col overflow-auto" ref={messagesListRef}>
                        {isLoading ? (
                            <div className="text-white">Loading...</div>
                        ) : (
                            <>
                                {chatMessages.map((message, index) => (
                                    <div
                                        key={index}
                                        className={`chat ${userId === message.SenderId ? "chat-end" : "chat-start"
                                            }`}
                                    >
                                        <div className="chat-header">
                                            <time className="text-xs opacity-50">
                                                {message.SentDate}
                                            </time>
                                        </div>
                                        <div className="chat-bubble text-white">
                                            {message.Message}
                                        </div>
                                        {userId === message.SenderId ? (
                                            ""
                                        ) : (
                                            <div className="chat-footer opacity-50">Leido</div>
                                        )}
                                    </div>
                                ))}
                            </>
                        )}
                    </div>
                    <div className="flex-1 flex items-center justify-between">
                        <textarea
                            ref={textareaRef}
                            className="w-[80%] p-2 resize-none bg-transparent border-2 border-gray-600 rounded-lg text-dark placeholder-gray-600"
                            rows={1}
                            placeholder="Escribe un mensaje..."
                            value={messageInput}
                            onChange={handleMessageChange}
                            onKeyDown={handleKeyDown}
                        ></textarea>
                        <Button className="flex items-center gap-3" onClick={sendMessageToServer}>
                            Enviar
                            <PaperAirplaneIcon strokeWidth={2} className="h-5 w-5"></PaperAirplaneIcon>
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Chat;