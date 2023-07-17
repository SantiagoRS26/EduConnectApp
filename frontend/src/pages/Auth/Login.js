import React from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import COVER_IMAGE from '../../assets/img/canva.jpg';
import { Input, Button, Loading, Text } from '@nextui-org/react';
import { Checkbox } from '@material-tailwind/react';
import accountController from '../../services/api/accountController';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import IconGoogle from '../../assets/img/illustrations/svggoogle.svg'

const Login = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const navigate = useNavigate();
    const [loginError, setLoginError] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const handleGoogleLogin = () => {
        try {
            window.location.href = 'https://localhost:7057/auth/google-login';
        } catch (error) {
            console.log("Pasa directamente a este error: ", error);
        }
    };

    const onSubmit = async (data) => {
        try {
            setIsLoading(true);
            const response = await accountController.login(data);
            localStorage.setItem('token', response.token);
            navigate('/user/dashboard');

        } catch (error) {
            if (error.response && error.response.status === 404) {
                setLoginError('Usuario no encontrado, verifique las credenciales');
            } else if (error.response.status === 404) {
                console.error("EL ERROR ES:  " + error);
            }
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="w-full h-screen flex flex-col items-start md:flex-row bg-login-bg bg-cover">
            <div className='relative w-full h-full flex flex-col md:w-1/2' >
                <div className='absolute top-[20%] left-[10%] flex flex-col' >
                    <h1 className='text-4xl text-white font-bold my-4' > Convierte tus ideas en realidad</h1>
                    <p className='text-xl text-white font-normal'>Aqui va una frase toda inspiradora que no se me ocurrio</p>
                </div>
                <img src={COVER_IMAGE} className="w-full h-full object-cover" alt="" />
            </div>
            <div className='w-full h-full flex flex-col p-20 justify-between md:w-1/2 backdrop-blur-md bg-white/60 space-y-5'>
                <h1 className='text-xl text- [#060606] font-semibold'>EduConnect</h1>
                <div className='w-full flex flex-col'>
                    <div className='w-full flex flex-col mb-5'>
                        <h3 className='text-3xl font-semibold mb-4'>Iniciar Sesion</h3>
                        <p className='text-sm mb-5' >Bienvenido de nuevo, por favor introduzca sus datos.</p>
                    </div>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <div className='w-full flex flex-col space-y-8'>
                            <div className='w-full flex flex-col'>
                                <Input status={errors.email ? 'error' : 'default'} helperText={errors.email ? <Text size="$xs" color="error">{errors.email.message}</Text> : ''} bordered type="email" labelPlaceholder="Correo" {...register('email', {
                                    required: 'El correo es requerido',
                                    pattern: {
                                        value: /^\S+@\S+$/i,
                                        message: 'El correo no es válido'
                                    }
                                })} />

                            </div>
                            <div className='w-full flex flex-col'>
                                <Input.Password status={errors.password ? 'error' : 'default'} helperText={errors.password ? <Text size="$xs" color="error">{errors.password.message}</Text> : ''} bordered labelPlaceholder="Contraseña" {...register('password', {
                                    required: 'La contraseña es requerida'
                                })} />
                            </div>
                            <div className="w-full flex items-center justify-between">
                                <div className="w-full flex items-center">
                                    <Checkbox type='checkbox'/* {...register('remember',{})} */></Checkbox>
                                    <p className="text-sm">Recuerdame por 30 dias</p>
                                </div>
                                <p className="text-sm font-medium whitespace-nowrap cursor-pointer underline underline-offset-2">Olvide la contraseña</p>
                            </div>
                        </div>
                        <div className="w-full flex flex-col my-4">
                            {loginError && <Text color="error">{loginError}</Text>}
                            <Button className="bg-cyan-400 my-4 text-lg h-12" type="submit">
                                {isLoading ? <Loading color="currentColor" /> : 'Iniciar Sesión'}
                            </Button>
                            <Button className="bg-black text-lg h-12">Registrarse</Button>
                        </div>
                    </form>
                    <div className="w-full flex flex-col items-center justify-center relative py-2">
                        <div className="divider">O Tambien</div>
                    </div>
                    <div className="w-full flex flex-col my-5">
                        <Button className="bg-white text-black text-lg border-black border-solid h-12 flex" onPress={handleGoogleLogin}>
                            <img className='h-full w-8 mx-1' src={IconGoogle} alt="Google Icon" />
                            Iniciar Sesion con Google
                        </Button>
                    </div>
                </div>
                <div className="w-full flex items-center justify-center my-2">
                    <p className="text-sm font-normal ">No tienes cuenta? <span className="font-semibold underline underline-offset-2 cursor-pointer"> Registrate gratis</span></p>
                </div>
            </div>
        </div>
    );
};

export default Login;
