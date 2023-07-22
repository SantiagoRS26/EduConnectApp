import React from "react";
import ReactDOM from "react-dom/client";
import AppRoutes from './routes/AppRoutes';
import "./styles/main.css";
import { NextUIProvider } from "@nextui-org/react";

ReactDOM.createRoot(document.getElementById("root")).render(
  <NextUIProvider>
    <AppRoutes />
  </NextUIProvider>
);