import axios from "axios";

const BASE_URL = process.env.REACT_APP_API_URL_COLLEGES_CONTROLLER;

const collegesController = {
    colleges: async () =>{
        try {
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