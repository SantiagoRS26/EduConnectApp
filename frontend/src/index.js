import React, { Component } from "react";
import ReactDOM from "react-dom/client";
import AppRoutes from './routes/AppRoutes';
import "./styles/main.css";
import { NextUIProvider } from "@nextui-org/react";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <NextUIProvider>
      <AppRoutes />
    </NextUIProvider>
  </React.StrictMode>
);