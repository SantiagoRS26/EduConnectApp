import React, { useEffect, useState } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import Dashboard from '../pages/Dashboard/Home';
import ChatList from '../pages/Dashboard/ChatList';
import Profile from '../pages/Dashboard/Profile';
import accountController from '../services/api/accountController';
import Chat from '../pages/Dashboard/Chat';

const UserRoutes = () => {

  const [isAuthenticated, setIsAuthenticated] = useState(null);


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
    <Routes>
      <Route path="dashboard" element={<Dashboard />} />
      <Route path="profile" element={<Profile />} />
      <Route path="chats" element={<ChatList />} />
      <Route path="chat/:chatId" element={<Chat />} />
    </Routes>
  );
};

export default UserRoutes;
