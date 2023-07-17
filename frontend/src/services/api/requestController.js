import axios from "axios";

const BASE_URL = "https://localhost:7057/api/Request";

const requestController = {
    getRequest: async () =>{
        try {
            const token = localStorage.getItem('token');
            const response = await axios.get(`${BASE_URL}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    }
}

export default requestController;