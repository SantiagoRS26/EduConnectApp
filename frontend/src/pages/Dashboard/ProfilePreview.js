import React, { useEffect, useState } from "react";
import SideBar from '../../components/SiderBar';
import { Button, Input } from '@nextui-org/react';
import { Avatar } from "@material-tailwind/react";
import accountController from "../../services/api/accountController";
import { useForm } from 'react-hook-form';

import Skeleton from 'react-loading-skeleton';
import "../../../node_modules/react-loading-skeleton/dist/skeleton.css";

const Profile = () => {

  const [userData, setUserData] = useState(null);
  const { register, handleSubmit, formState: { errors } } = useForm();
  const [isLoading, setIsLoading] = useState(false);


  const onSubmit = async (data) => {
    try {
      setIsLoading(true);

      const response = await accountController.login(data);
      localStorage.setItem('token', response.token);
    } catch (error) {
      console.error(error);

    } finally {
      setIsLoading(false);
    }
  }


  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const data = await accountController.userData();
        if (data) {
          setUserData(data);
        }

      } catch (error) {
        console.log(error);
      }
    };

    fetchUserData();
  }, []);


  return (
    <div className="flex">
      <div className="w-[20rem]">
        <SideBar />
      </div>
      <div className='flex-1 flex bg-white h-screen items-center justify-center'>
        <div className='w-1/2 flex flex-col pl-20 space-y-10'>
          <div className='w-full'>
            <h1 className='text-2xl font-bold'>Mi perfil</h1>
            <p className='text-gray-500'>Administrar la configuración de su perfil</p>
          </div>
          <div className='flex flex-col space-y-5'>
            <h1 className='text-2xl font-bold'>Tu foto de perfil</h1>
            <div className='w-full flex'>
              <div className='w-1/2 flex items-center justify-center'>
                <Avatar
                  src="https://i.pravatar.cc/150?u=a04258114e29026702d"
                  className="h-100 w-100"
                />
              </div>
              <div className='w-1/2 flex flex-col items-center justify-center space-y-4'>
                <Button className='bg-cyan-500 text-lg'>Cambiar</Button>
                <Button className='bg-red-600 text-lg'>Eliminar</Button>
              </div>
            </div>
            <p className='text-gray-500'>Agrega tu foto. El tamaño recomendado es 250x250 px</p>
          </div>
          <form /* onSubmit={handleSubmit(onSubmit)} */ className='w-full flex flex-col space-y-4'>
            <Input
              clearable
              /* helperText="Please enter your name" */
              label="Nombre"
              className='w-30'
              initialValue={userData ? (userData.Name) : ("Prueba")}
            />
            <Input
              clearable
              /* helperText="Please enter your name" */
              label="Apellidos"
              className='w-30'
              placeholder={userData ? (userData.LastName) : (
                <Skeleton containerClassName="flex-1" count={1} />
              )}
            />
            <Input
              clearable
              /* helperText="Please enter your name" */
              label="Email"
              className='w-30'
              placeholder={userData ? (userData.Email) : (
                <Skeleton containerClassName="flex-1" count={1} />
              )}
            />
          </form>
        </div>
        <div className='w-1/2'>

        </div>
      </div>
    </div>
  );
};
export default Profile;
