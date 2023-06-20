import React, { useEffect, useState } from "react";
import SideBar from '../../components/SiderBar';
import {
  List,
  ListItem,
  ListItemPrefix,
  Avatar,
  Card,
  Typography,
} from "@material-tailwind/react";
import { useNavigate } from "react-router-dom";
import chatController from "../../services/api/chatController";

import Skeleton from 'react-loading-skeleton';
import "../../../node_modules/react-loading-skeleton/dist/skeleton.css";

const ChatList = () => {
  const [chatsUser, setChatsUser] = useState(null);
  const navigate = useNavigate();

  const handleChatClick = (chatId) => {
    navigate(`/user/chat/${chatId}`);
  };

  useEffect(() => {
    const fetchChatsUser = async () => {
      try {
        const data = await chatController.GetChats();
        if (data) {
          setChatsUser(data.$values);
        }

      } catch (error) {
        console.log(error);
      }
    };

    fetchChatsUser();
  }, []);

  return (
    <div className="flex">
      <div className="w-[20rem]">
        <SideBar />
      </div>
      <Card className="h-screen flex-1 flex justify-center items-center bg-gradient-to-r from-blue-200 to-cyan-200">
        <List className="w-4/5 h-4/5 my-20 backdrop-blur-md bg-white/25 space-y-5 rounded-3xl overflow-y-auto">

          {chatsUser ? (chatsUser.map((chat) => (
            <ListItem key={chat.ChatId} onClick={() => handleChatClick(chat.ChatId)}>
              <ListItemPrefix>
                <Avatar withBorder={true} color="green" variant="circular" alt={chat.name} src={chat.OtherImage || "https://t3.ftcdn.net/jpg/03/39/45/96/360_F_339459697_XAFacNQmwnvJRqe1Fe9VOptPWMUxlZP8.jpg"} />
              </ListItemPrefix>
              <div>
                <Typography variant="h6" color="blue-gray">
                  {chat.name}
                </Typography>
                <Typography variant="small" color="gray" className="font-normal">
                  Valor Prueba
                </Typography>
              </div>
            </ListItem>
          ))) : (
            <>
              <Skeleton className="space-y-10" height={60} count={5} />
            </>
          )}

        </List>
      </Card>
    </div>
  );
};

export default ChatList;
