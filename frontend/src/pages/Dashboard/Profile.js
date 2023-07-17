import React, { useState, useRef, useContext } from "react";

import SideBar from "../../components/SiderBar";

import {
    Breadcrumbs,
    Tabs,
    TabsHeader,
    TabsBody,
    Tab,
    TabPanel,
    Card,
    CardBody,
    Avatar,
    Button,
    Alert
} from "@material-tailwind/react";

import { Input } from "@nextui-org/react";

import { useForm } from 'react-hook-form';

import { UserCircleIcon, AcademicCapIcon } from "@heroicons/react/24/outline";

import { ExclamationTriangleIcon } from "@heroicons/react/24/solid"

import Map from "../../components/Map";

import accountController from "../../services/api/accountController";

import defaultPhoto from "../../assets/img/avatar/avatarDefault.jpg";

import { UserContext } from "../../routes/UserRoutes";

const Profile = () => {
    const [activeTab, setActiveTab] = useState("1");
    const { register, handleSubmit, formState: { errors } } = useForm();
    const [selectedImage, setSelectedImage] = useState(null);
    const [pictureProfile, setPictureProfile] = useState(null);
    const [open, setOpen] = useState(false);
    const inputFileRef = useRef(null);
    const [selectedCollege, setSelectedCollege] = useState(null);
    const { userData } = useContext(UserContext);

    const handleSelectedCollege = (college) => {
        setSelectedCollege(college);
    };


    const onSubmit = async (data) => {
        if (pictureProfile) {
            const formData = new FormData();
            formData.append("file", pictureProfile);
            try {
                const response = await accountController.uploadprofilePicture(formData);
                console.log(response);
            } catch (error) {
                console.log(error);
            }
        }
        /* try {
            const response = await accountController.updateUser(data);
            console.log(response);
        } catch (error) {
            throw error;
        } */
    };

    return (
        <div className="flex bg-white">
            <div className="flex-initial">
                <SideBar />
            </div>
            <div className="flex-grow bg-white flex justify-center flex-col items-center space-y-3 mt-8">
                <div className="flex w-full md:w-11/12">
                    <div className="flex p-5 bg-gradient-to-r from-teal-200 to-teal-100 w-full flex-col space-y-3 rounded-2xl">
                        <h1 className="text-2xl">
                            Configuración de cuenta
                        </h1>
                        <Breadcrumbs separator="/" className="bg-transparent m-0">
                            <a href="#" className="opacity-60 m-0">
                                Home
                            </a>
                            <a href="#" className="m-0">
                                Profile
                            </a>
                        </Breadcrumbs>
                    </div>
                </div>
                <Tabs className="w-full md:w-11/12" value={activeTab}>
                    <TabsHeader className="rounded-none rounded-t-md border-blue-gray-50 bg-transparent p-0 border-x-[1px] border-t-[1px]"
                        indicatorProps={{
                            className: "bg-transparent border-b-2 border-blue-500 shadow-none rounded-none",
                        }}
                    >
                        <Tab
                            value='1'
                            className={activeTab === '1' ? "text-blue-500 my-0 w-auto px-5 py-3" : "my-0 w-auto px-5 py-3"}
                            onClick={() => setActiveTab('1')}
                        >
                            <div className="flex items-center gap-2">
                                <UserCircleIcon className="w-5 h-5"></UserCircleIcon>
                                Perfil
                            </div>
                        </Tab>
                        <Tab
                            value='2'
                            className={activeTab === '2' ? "text-blue-500 my-0 w-auto px-5 py-3" : "my-0 w-auto px-5 py-3"}
                            onClick={() => setActiveTab('2')}
                        >
                            <div className="flex items-center gap-2">
                                <AcademicCapIcon className="w-5 h-5"></AcademicCapIcon>
                                Colegio
                            </div>
                        </Tab>
                    </TabsHeader>
                    <TabsBody className="border rounded-b-md">
                        <TabPanel value="1" className="flex w-full flex-col space-y-5">
                            <form onSubmit={handleSubmit(onSubmit)} className="flex w-full flex-col space-y-5">
                                <div className="flex w-full gap-5 flex-col md:flex-row">
                                    <Card className="flex w-full md:w-1/2 border-[1px] shadow-sm">
                                        <CardBody className="flex flex-col items-center">
                                            <h1 className="text-xl text-black font-medium w-full">Cambiar Foto</h1>
                                            <p className="text-gray-600 w-full">Cambia tu foto de perfil desde aquí</p>
                                            <div className="flex flex-col w-4/6 items-center space-y-5 justify-center">
                                                <Avatar
                                                    src=
                                                    {
                                                        selectedImage ?
                                                            (selectedImage)
                                                            :
                                                            (userData && userData.photo ? `https://localhost:7057/pictures/${userData.photo}` : (defaultPhoto))
                                                    }
                                                    className="w-[120px] h-[120px] mt-5"
                                                />
                                                <div className="flex space-x-3">
                                                    {!open && (
                                                        <div className="flex">
                                                            <div className="flex space-x-4 basis-3">
                                                                <label
                                                                    className="cursor-pointer duration-200 normal-case py-2 px-6 text-xs font-bold text-white rounded-lg bg-blue-400 hover:bg-blue-600 shadow-none hover:shadow-none"
                                                                >
                                                                    <input
                                                                        type="file"
                                                                        accept="image/*"
                                                                        ref={inputFileRef}
                                                                        className="hidden"
                                                                        onChange={(e) => {
                                                                            const file = e.target.files[0];
                                                                            if (file && file.type.startsWith("image/")) {
                                                                                const imageUrl = URL.createObjectURL(file);
                                                                                setPictureProfile(file);
                                                                                setSelectedImage(imageUrl);
                                                                                //URL.revokeObjectURL(file); USAR PARA LIBERAR MEMORIA
                                                                            } else {
                                                                                setOpen(true);
                                                                            }
                                                                        }}
                                                                    />
                                                                    Update
                                                                </label>
                                                                <Button className="normal-case py-2 m-0 bg-transparent border-solid border-[1px] border-orange-400 hover:shadow-transparent shadow-none text-orange-400 hover:text-white hover:bg-orange-400"
                                                                    onClick={() => {
                                                                        setSelectedImage(null);
                                                                        setPictureProfile(null);
                                                                        if (inputFileRef.current) {
                                                                            inputFileRef.current.value = "";
                                                                        }
                                                                    }}
                                                                >Reset</Button>
                                                            </div>
                                                        </div>
                                                    )}

                                                    {open && (
                                                        <Alert
                                                            className="flex"
                                                            color="red"
                                                            icon={<ExclamationTriangleIcon className="h-6 w-6" />}
                                                            open={open}
                                                            onClose={() => setOpen(false)}
                                                            animate={{
                                                                mount: { y: 0 },
                                                                unmount: { y: 100 },
                                                            }}
                                                        >
                                                            El archivo seleccionado no es valido!
                                                        </Alert>
                                                    )}

                                                </div>
                                                <p className="text-gray-600 text-center text-sm">Permitido JPG, GIF o PNG. Tamaño máximo de 800K</p>
                                            </div>
                                        </CardBody>
                                    </Card>
                                    <Card className="flex w-full md:w-1/2 flex-col border-[1px] shadow-sm">
                                        <CardBody className="flex flex-col items-center">
                                            <h1 className="text-xl text-black font-medium w-full">Cambiar la contraseña</h1>
                                            <p className="text-gray-600 w-full">Para cambiar su contraseña por favor confirme aquí</p>
                                            <div className="w-full flex flex-col space-y-4 my-4">
                                                <Input.Password label="Contraseña actual" {...register("Password")} />
                                                <Input.Password label="Nueva contraseña" /* {...register("newPassword")} */ />
                                                <Input.Password label="Confirmar contraseña" /* {...register("confirmPassword")} */ />
                                            </div>
                                        </CardBody>
                                    </Card>
                                </div>

                                <Card className="flex w-full flex-col border-[1px] shadow-sm">
                                    <CardBody className="w-full flex flex-col items-center">
                                        <h1 className="text-xl text-black font-medium w-full">Detalles personales</h1>
                                        <p className="text-gray-600 w-full">Para cambiar sus datos personales, edite y guarde desde aquí</p>
                                        <div className="flex w-full flex-col space-y-5">
                                            <div className="flex w-full space-x-5">
                                                <Input label="Nombre" placeholder={userData?.name} fullWidth="true" {...register("Name")}></Input>
                                                <Input label="Apellidos" placeholder={userData?.lastName} fullWidth="true" {...register("LastName")} ></Input>
                                            </div>
                                        </div>
                                    </CardBody>
                                </Card>
                                <Card className="flex w-full border-[1px] shadow-sm">
                                    <CardBody>
                                        <h1 className="text-xl text-black font-medium w-full">Seleccionar colegio</h1>
                                        <p className="text-gray-600 w-full mb-8">Aquí podra seleccionar un colegio</p>
                                        <div className="flex w-full h-[600px]">
                                            <Map onCollegeSelect={handleSelectedCollege} />
                                        </div>
                                        <h1 className="text-2xl text-black font-medium w-full text-center mt-8">Colegio Seleccionado</h1>
                                        {userData && userData.collegeId ?
                                            (selectedCollege && (
                                                <div>
                                                    <p className="text-gray-600 text-center text-lg">{selectedCollege.name}</p>
                                                    <p className="text-gray-600 text-center text-lg">{selectedCollege.address}</p>
                                                    <p className="text-gray-600 text-center text-lg">{selectedCollege.additionalInfo}</p>
                                                </div>

                                            ))
                                            :
                                            <h1>Aun no tiene algun colegio seleccionado</h1>}

                                    </CardBody>
                                </Card>
                                <div className="flex w-full items-center justify-end space-x-5">
                                    <Button type="submit" className="normal-case text-sm bg-blue-400 hover:bg-blue-600 shadow-none hover:shadow-none">Guardar</Button>
                                    <Button
                                        className="normal-case text-sm bg-orange-100/40 border-orange-400 hover:shadow-transparent shadow-none text-orange-400 hover:text-white hover:bg-orange-4040"
                                    >Cancelar
                                    </Button>
                                </div>
                            </form>
                        </TabPanel>
                    </TabsBody>
                </Tabs>
            </div>
        </div>
    );
}
export default Profile;