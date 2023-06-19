import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import AuthRoutes from './AuthRoutes';
import UserRoutes from './UserRoutes';
import Index from '../pages/Index';

const AppRoutes = () => {
    return (
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Index/>} />
          <Route path="/auth/*" element={<AuthRoutes/>} />
          <Route path="/user/*" element={<UserRoutes/>} />
        </Routes>
      </BrowserRouter>
    );
};
  
export default AppRoutes;
