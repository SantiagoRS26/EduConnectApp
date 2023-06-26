import React from 'react';
import SideBar from '../../components/SiderBar';
import createSignalRConnection from '../../utils/SignalRClient';

const Home = () => {
  const connection = createSignalRConnection();
  connection.start()
    .then(() => {
    })
    .catch((error) => {
      console.error("Error al establecer la conexión con el servidor SignalR", error);
    });

  return (
    <div className="flex">
      <div className="w-[20rem]">
        <SideBar />
      </div>
      <div className="flex-1 w-screen h-screen">
        <h1>HOLA</h1>
      </div>
    </div>
  );
};


export default Home;
