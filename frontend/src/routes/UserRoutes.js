import React, { useEffect, useState } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import Dashboard from '../pages/Dashboard/Home';
import ChatList from '../pages/Dashboard/ChatList';
import Profile from '../pages/Dashboard/Profile';
import accountController from '../services/api/accountController';

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

  if(isAuthenticated === null){
    return(
      <div>Cargando</div>
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
    </Routes>
  );
};

export default UserRoutes;
