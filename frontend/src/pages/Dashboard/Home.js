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
    <div className="flex w-full h-full overflow-hidden">
      <div className='flex-initial'>
        <SideBar />
      </div>

      <div className="flex-grow">
        <div className='w-full flex flex-col justify-center items-center mt-10'>
          <div className='flex w-11/12 bg-[#fb7185]/40 px-24 py-16 rounded-3xl'>
            <div className='w-1/2 flex flex-col gap-y-5'>
              <h1 className='text-2xl text-[#fb7185] font-semibold'>Bienvenido de vuelta!</h1>
              <p className='text-lg'>Es un placer tenerte nuevamente aqui en nuestra plataforma</p>
            </div>
            <div className='w-1/2'>
              
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};


export default Home;
