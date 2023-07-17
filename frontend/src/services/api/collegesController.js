import axios from "axios";

const BASE_URL = "https://localhost:7057/api/Colleges";

const collegesController = {
    colleges: async () =>{
        try {
            console.log("Llamada al servidor mapa...");
            const token = localStorage.getItem('token');
            const response = await axios.get(`${BASE_URL}/colleges`, {
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

export default collegesController;