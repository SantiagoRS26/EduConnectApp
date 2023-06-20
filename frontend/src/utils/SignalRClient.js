import React from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const token = localStorage.getItem('token');

const connection = new HubConnectionBuilder()
    .withUrl("https://localhost:7057/Hubs/ChatHub", {
        accessTokenFactory: () => token,
    })
    .configureLogging(LogLevel.Information)
    .build();

connection.start()
    .then(() => {
        console.log("Conexión establecida con el servidor SignalR");
    })
    .catch((error) => {
        console.error("Error al establecer la conexión con el servidor SignalR", error);
    });

export default connection;