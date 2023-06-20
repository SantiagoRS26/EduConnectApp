import axios from "axios";

const BASE_URL = "https://localhost:7057/api/Chat";

const token = localStorage.getItem('token');

const chatController = {
    GetChats: async () => {
        try {
            const response = await axios.get(`${BASE_URL}/Chats`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },
    messages: async (data) =>{
        try {
            const response = await axios.get(`${BASE_URL}/messages?idChat=${data}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            return response.data;
        } catch (error) {
            console.log("Error del llamado");
            throw error;
        }
    }
}

export default chatController;