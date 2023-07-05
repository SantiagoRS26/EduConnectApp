import React, { useEffect, useState } from "react";
import {
    Card,
    Typography,
    List,
    ListItem,
    ListItemPrefix,
    ListItemSuffix,
    Chip,
    Button
} from "@material-tailwind/react";
import {
    PresentationChartBarIcon,
    UserCircleIcon,
    Cog6ToothIcon,
    PowerIcon,
    ChatBubbleBottomCenterTextIcon,
    ArrowLeftIcon,
    ArrowRightIcon
} from "@heroicons/react/24/solid";

import { useNavigate, useLocation } from 'react-router-dom';

import defaultPhoto from "../assets/img/avatar/avatarDefault.jpg";

import { Avatar } from "@nextui-org/react";

import accountController from "../services/api/accountController";

import Skeleton from 'react-loading-skeleton';

import "../../node_modules/react-loading-skeleton/dist/skeleton.css";

const SideBar = ({ dataUser = () => { } }) => {
    const navigate = useNavigate();
    const location = useLocation();
    const [sidebarOpen, setSidebarOpen] = useState(localStorage.getItem('sidebarOpen') === 'true');
    const [userData, setUserData] = useState(null);

    useEffect(() => {
        localStorage.setItem('sidebarOpen', sidebarOpen);
    }, [sidebarOpen]);

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const data = await accountController.userData();
                if (data) {
                    setUserData(data);
                    dataUser(data);
                }

            } catch (error) {
                console.log(error);
            }
        };

        fetchUserData();
    }, []);

    return (
        <>
            <div className={`${sidebarOpen ? 'w-[20rem]' : 'w-[6rem]'} transition-all duration-300`}></div>
            <div className={`transition-all duration-300 flex-none h-full w-full ${sidebarOpen ? 'max-w-[20rem]' : 'max-w-[6rem]'}`}>
                <Card className={`transition-all duration-300 fixed h-full w-full p-4 shadow-2xl backdrop-blur-sm bg-white/10 text-white ${sidebarOpen ? 'max-w-[20rem]' : 'max-w-[6rem]'}`}>
                    <button
                        className={`border-2 border-gray-700/40 absolute inset-0 p-2 rounded-full backdrop-blur-sm bg-gray-500/80 z-[2000] flex justify-center items-center h-10 w-10 top-1/2 ${sidebarOpen ? 'left-[94%]' : 'left-[79%]'}`}
                        onClick={() => sidebarOpen ? setSidebarOpen(false) : setSidebarOpen(true)}
                    >
                        <ArrowLeftIcon className={`h-5 w-5 duration-500 ${sidebarOpen ? '' : 'rotate-180'}`} />
                    </button>
                    <div className="w-full items-center justify-center">
                        <div className={`mb-2 flex items-center justify-center ${sidebarOpen ? '' : 'invisible'}`}>
                            <Typography variant="h3" color="blue-gray" className={`${sidebarOpen ? 'text-opacity-100' : 'text-opacity-0'} duration-300`}>
                                EduConnect
                            </Typography>
                        </div>
                        <div className={`w-full flex justify-center flex-col pt-10 items-center`}>
                            <Avatar
                                className={`${sidebarOpen ? 'h-28 w-28' : 'w-16 h-16'} transition-all duration-300`}
                                src={userData ? (userData.photo ? `https://localhost:7057/pictures/${userData.photo}` : defaultPhoto) : (<Skeleton containerClassName="flex-1" circle="true" />)}
                            />
                            <Typography
                                className={`my-3 flex w-full justify-center duration-300 overflow-hidden truncate ${sidebarOpen ? 'text-opacity-100' : 'invisible text-opacity-0'}`} variant="h5" color="blue-gray"
                            >
                                {userData ? (userData.name + " " + userData.lastName) : (<Skeleton containerClassName="flex-1" count={1} />)}
                            </Typography>
                            <Typography className={`duration-300 ${sidebarOpen ? 'text-opacity-100' : 'invisible text-opacity-0'}`} color="blue-gray">
                                {userData ? (
                                    userData.role === "user" ? <span>Usuario</span> : null
                                ) : (
                                    <Skeleton containerClassName="flex-1" count={1} />
                                )}
                            </Typography>


                        </div>
                    </div>

                    <List className="h-3/5 flex transition-all duration-1000 justify-center space-y-3 w-full min-w-full">
                        <ListItem selected={location.pathname === '/user/dashboard'} onClick={() => navigate('/user/dashboard')}>
                            <ListItemPrefix>
                                <PresentationChartBarIcon className="h-5 w-5" />
                            </ListItemPrefix>
                            {sidebarOpen ? 'Inicio' : ''}
                        </ListItem>
                        <ListItem selected={location.pathname === '/user/chats'} onClick={() => navigate('/user/chats')}>
                            <ListItemPrefix>
                                <ChatBubbleBottomCenterTextIcon className="h-5 w-5" />
                            </ListItemPrefix>
                            {sidebarOpen ? 'Chats' : ''}
                            {sidebarOpen ? (<ListItemSuffix>
                                <Chip value="14" size="sm" variant="ghost" color="blue-gray" className="rounded-full" />
                            </ListItemSuffix>) : ''}
                        </ListItem>
                        <ListItem selected={location.pathname === '/user/profile'} onClick={() => navigate('/user/profile')}>
                            <ListItemPrefix>
                                <UserCircleIcon className="h-5 w-5" />
                            </ListItemPrefix>
                            {sidebarOpen ? 'Perfil' : ''}
                        </ListItem>
                        <ListItem>
                            <ListItemPrefix>
                                <Cog6ToothIcon className="h-5 w-5" />
                            </ListItemPrefix>
                            {sidebarOpen ? 'Configuración' : ''}
                        </ListItem>
                    </List>
                    <List className="flex-1 justify-end min-w-full truncate">
                        <ListItem>
                            <ListItemPrefix>
                                <PowerIcon className="h-5 w-5" />
                            </ListItemPrefix>
                            {sidebarOpen ? 'Cerrar Sesión' : ''}
                        </ListItem>
                    </List>
                </Card>
            </div>
        </>
    );
}

export default SideBar;