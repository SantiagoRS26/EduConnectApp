import React from 'react';
import SideBar from '../../components/SiderBar';

const Home = () => {
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
