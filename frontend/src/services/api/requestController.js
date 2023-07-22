import axios from "axios";

const BASE_URL = process.env.REACT_APP_API_URL_REQUEST_CONTROLLER;

const requestController = {
    getRequest: async () => {
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
    },
    sendRequest: async (data) => {
        try {
            const token = localStorage.getItem('token');
            const response = await axios.post(`${BASE_URL}/sendRequest`, data, {
                headers: {
                    Authorization: `Bearer ${token}`
                },
            });
            return response;
        } catch (error) {
            throw error;
        }
    }
}

export default requestController;