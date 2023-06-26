import axios from "axios";

const BASE_URL = "https://localhost:7057/api/Account";

const accountController = {
    login: async (data) => {
        try {
            const response = await axios.post(`${BASE_URL}/login`, data);
            return response.data;
        } catch (error) {
            throw error;
        }
    },
    register: async (data) => {
        try {
            const response = await axios.post(`${BASE_URL}/register`, data);
            return response.data;
        } catch (error) {
            throw error;
        }
    },
    updateUser: async (data) => {
        try {
            const token = localStorage.getItem('token');
            const response = await axios.post(`${BASE_URL}/updateUser`, data, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            return response.data;
        } catch (error) {
            console.log(error);
            throw error;
        }
    },
    validatejwt: async (token) => {
        try {
            const response = await axios.get(`${BASE_URL}/validatejwt`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            if (response.status === 200) {
                return true;
            }
            return false;
        } catch (error) {
            console.log(error);
            return false;
        }
    },
    userData: async () => {
        const token = localStorage.getItem('token');
        try {
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
    uploadprofilePicture: async (data) => {
        const token = localStorage.getItem('token');
        try {
            console.log(data);
            const response = await axios.post(`${BASE_URL}/uploadprofilepicture`, data, {
                headers: {
                    Authorization: `Bearer ${token}`
                    //"Content-Type": "multipart/form-data"
                },
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },
    updateUser: async (data) => {
        const token = localStorage.getItem('token');
        try {
            const response = await axios.patch(`${BASE_URL}/updateUser`, data, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "multipart/form-data"
                },
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    }
}

export default accountController;