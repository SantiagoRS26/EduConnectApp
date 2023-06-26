import React from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const createSignalRConnection = () => {
    const token = localStorage.getItem('token');
  
    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7057/Hubs/ChatHub", {
        accessTokenFactory: () => token,
      })
      .configureLogging(LogLevel.Error)
      .build();
  
    return connection;
  };

export default createSignalRConnection;