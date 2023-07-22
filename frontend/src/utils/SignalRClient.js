import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const createSignalRConnection = () => {
    const token = localStorage.getItem('token');
    const urlHub = process.env.REACT_APP_API_URL_SIGNALR_HUB;
    const connection = new HubConnectionBuilder()
      .withUrl(urlHub, {
        accessTokenFactory: () => token,
      })
      .configureLogging(LogLevel.Error)
      .build();
  
    return connection;
  };

export default createSignalRConnection;