import React, { useEffect, useState, createContext } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import Dashboard from '../pages/Dashboard/Home';
import ChatList from '../pages/Dashboard/ChatList';
import Profile from '../pages/Dashboard/Profile';
import accountController from '../services/api/accountController';
import Chat from '../pages/Dashboard/Chat';
import SideBar from '../components/SiderBar';

export const UserContext = createContext();

const UserRoutes = () => {

  const [isAuthenticated, setIsAuthenticated] = useState(null);
  const [userData, setUserData] = useState(null);

  useEffect(() => {
    const fetchUserData = async () => {
        try {
            console.log('Realizando petición al servidor...');
            const data = await accountController.userData();
            if (data) {
                setUserData(data);
            }

        } catch (error) {
            console.log(error);
        }
    };

    fetchUserData();
}, []);


  useEffect(() => {
    const checkAuthentication = async () => {
      try {
        const token = localStorage.getItem('token');
        const result = await accountController.validatejwt(token);
        setIsAuthenticated(result);
      } catch (error) {
        throw error;
      }
    };
    console.log("Se ejecuta useEffect de comprobar el token");
    checkAuthentication();
  }, []);

  if (isAuthenticated === null) {
    return (

      <div
        className="inline-block h-8 w-8 animate-spin rounded-full border-4 border-solid border-current border-r-transparent align-[-0.125em] motion-reduce:animate-[spin_1.5s_linear_infinite]"
        role="status">
        <span
          className="!absolute !-m-px !h-px !w-px !overflow-hidden !whitespace-nowrap !border-0 !p-0 ![clip:rect(0,0,0,0)]"> Loading...</span>
      </div>

    );
  }

  if (!isAuthenticated) {
    return <Navigate to="/auth/login" />;
  }

  return (
    <UserContext.Provider value={{userData}}>
      <Routes>
        <Route path="dashboard" element={<Dashboard />} />
        <Route path="profile" element={<Profile />} />
        <Route path="chats" element={<ChatList />} />
        <Route path="chat/:chatId" element={<Chat />} />
      </Routes>
    </UserContext.Provider>
  );
};

export default UserRoutes;
