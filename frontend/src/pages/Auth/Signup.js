import React from 'react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import COVER_IMAGE from '../../assets/img/canva.jpg';
import { Input, Button, Loading, Text } from '@nextui-org/react';
import { Alert } from '@material-tailwind/react';
import accountController from '../../services/api/accountController';
import { useNavigate } from 'react-router-dom';
import IconGoogle from '../../assets/img/illustrations/svggoogle.svg'
import { ExclamationTriangleIcon } from "@heroicons/react/24/solid";

const Signup = () => {
  const { register, handleSubmit, formState: { errors } } = useForm();
  const navigate = useNavigate();
  const [registerError, setRegisterError] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [alert, setAlert] = useState(false);

  const onSubmit = async (data) => {
    try {
      setIsLoading(true);
      const response = await accountController.register(data);
      if (response.status == 409) setAlert(true);
      if (response.status == 200) {
        localStorage.setItem('token', response.data.token);
        navigate('/user/dashboard');
      }
    } catch (error) {
      throw error;
    }
    finally {
      setIsLoading(false);
    }
  }

  const handleGoogleLogin = () => {
  };

  return (
    <div className="w-full h-screen flex flex-col items-start md:flex-row bg-login-bg bg-cover">
      <Alert
        variant="gradient"
        className='absolute bottom-0 right-0 z-20 w-auto m-5 text-center'
        color="red"
        open={alert}
        icon={<ExclamationTriangleIcon className="h-6 w-6" />}
        action={
          <button
            className="top-3 right-3 px-3 rounded-lg hover:bg-red-600 duration-200 text-sm active:bg-red-700"
            onClick={() => setAlert(false)}
          >
            Cerrar
          </button>
        }
      >
        El correo ingresado ya esta registrado
      </Alert>
      <div className='w-full h-full flex flex-col p-20 justify-between md:w-1/2 backdrop-blur-md bg-white/60 space-y-5'>
        <h1 className='text-xl text- [#060606] font-semibold'>EduConnect</h1>
        <div className='w-full flex flex-col'>
          <div className='w-full flex flex-col mb-5'>
            <h3 className='text-3xl font-semibold mb-4'>Registrarse</h3>
            <p className='text-sm mb-5' >Explora oportunidades escolares: encuentra cupos para tus hijos.</p>
          </div>
          <form onSubmit={handleSubmit(onSubmit)}>
            <div className='w-full flex flex-col space-y-11'>
              <div className='w-full flex gap-x-5'>
                <div className='w-full flex flex-col'>
                  <Input
                    status={errors.name ? 'error' : 'default'}
                    helperText={errors.name ? <Text size="$xs" color="error">{errors.name.message}</Text> : ''}
                    bordered
                    labelPlaceholder="Nombre"
                    {...register('name', {
                      required: 'El nombre es requerido',
                      maxLength: {
                        value: 20,
                        message: 'El nombre no puede superar los 20 caracteres',
                      },
                      minLength: {
                        value: 3,
                        message: 'El nombre debe tener al menos 3 caracteres',
                      },
                    })}
                  />

                </div>
                <div className='w-full flex flex-col'>
                  <Input status={errors.lastname ? 'error' : 'default'} helperText={errors.lastname ? <Text size="$xs" color="error">{errors.lastname.message}</Text> : ''} bordered labelPlaceholder="Apellido" {...register('lastname', {
                    required: 'El apellido es requerido',
                    maxLength: {
                      value: 20,
                      message: 'El/Los apellidos no puede superar los 20 caracteres'
                    },
                    minLength: {
                      value: 3,
                      message: 'El/Los apellidos deben tener al menos 3 caracteres'
                    }
                  })} />
                </div>
              </div>
              <div className='w-full flex gap-x-5'>
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
                    required: 'La contraseña es requerida',
                    pattern: {
                      value: /^(?=.*[A-Z]).{8,}$/,
                      message:
                        'La contraseña debe contener al menos 8 caracteres y una letra mayúscula',
                    },
                  })} />
                </div>
              </div>
            </div>
            <div className="w-full flex flex-col my-4">
              {registerError && <Text color="error">{registerError}</Text>}
              <Button className="bg-cyan-400 my-4 text-lg h-12" type="submit">
                {isLoading ? <Loading color="currentColor" /> : 'Registrarse'}
              </Button>
            </div>
          </form>
          <div className="w-full flex flex-col items-center justify-center relative py-2">
            <div className="divider">O Tambien</div>
          </div>
          <div className="w-full flex flex-col my-5">
            <Button className="bg-white text-black text-lg border-black border-solid h-12 flex" onPress={handleGoogleLogin}>
              <img className='h-full w-8 mx-1' src={IconGoogle} alt="Google Icon" />
              Registrarse con Google
            </Button>
          </div>
        </div>
        <div className="w-full flex items-center justify-center my-2">
          <p className="text-sm font-normal ">Ya tienes cuenta? <span className="font-semibold underline underline-offset-2 cursor-pointer" onClick={() => { navigate('/auth/login') }}> Inicia Sesion</span></p>
        </div>
      </div>
      <div className='relative w-full h-full flex flex-col md:w-1/2' >
        <div className='absolute top-[20%] left-[10%] flex flex-col' >
          <h1 className='text-4xl text-white font-bold my-4' > Convierte tus ideas en realidad</h1>
          <p className='text-xl text-white font-normal'>Aqui va otra frase toda inspiradora que no he pensado</p>
        </div>
        <img src={COVER_IMAGE} className="w-full h-full object-cover" alt="" />
      </div>
    </div>
  );
};

export default Signup;
