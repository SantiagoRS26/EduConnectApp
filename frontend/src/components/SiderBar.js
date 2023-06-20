import React, { useEffect, useState } from "react";
import {
    Card,
    Typography,
    List,
    ListItem,
    ListItemPrefix,
    ListItemSuffix,
    Chip
} from "@material-tailwind/react";
import {
    PresentationChartBarIcon,
    UserCircleIcon,
    Cog6ToothIcon,
    PowerIcon,
    ChatBubbleBottomCenterTextIcon,
} from "@heroicons/react/24/solid";

import { useNavigate, useLocation } from 'react-router-dom';

import { Avatar } from "@nextui-org/react";

import accountController from "../services/api/accountController";

import Skeleton from 'react-loading-skeleton';
import "../../node_modules/react-loading-skeleton/dist/skeleton.css";

const SideBar = () => {
    const navigate = useNavigate();
    const location = useLocation();

    const [userData, setUserData] = useState(null);

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
        <div className="bg-gradient-to-tr from-gray-50 to-gray-200 fixed h-full w-full max-w-[20rem] ">
            <Card className="fixed h-full w-full max-w-[20rem] p-4 shadow-2xl backdrop-blur-sm bg-white/10 text-white ">
                <div className="w-full h    -1/4 items-center justify-center">
                    <div className="mb-2 flex items-center justify-center">
                        <Typography variant="h3" color="blue-gray">
                            EduConnect
                        </Typography>
                    </div>
                    <div className="w-full flex justify-center flex-col items-center my-10">
                        <Avatar
                            src="https://i.pravatar.cc/150?u=a04258114e29026702d"
                            css={{ size: "$20" }}
                            bordered
                            color="success"
                        />
                        <Typography className="my-3 flex w-full justify-center" variant="h5" color="blue-gray">
                            {userData ? (userData.Name + " " + userData.LastName) : (<Skeleton containerClassName="flex-1" count={1} />)}
                        </Typography>
                        <Typography color="blue-gray">
                            {userData ? (
                                userData.Role.RoleName === "user" ? <span>Usuario</span> : null
                            ) : (
                                <Skeleton containerClassName="flex-1" count={1} />
                            )}
                        </Typography>


                    </div>
                </div>

                <List className="h-3/5 flex justify-center space-y-3">
                    <ListItem selected={location.pathname === '/user/dashboard'} onClick={() => navigate('/user/dashboard')}>
                        <ListItemPrefix>
                            <PresentationChartBarIcon className="h-5 w-5" />
                        </ListItemPrefix>
                        <Typography color="blue-gray" className="mr-auto font-normal">
                            Inicio
                        </Typography>
                    </ListItem>
                    <ListItem selected={location.pathname === '/user/chats'} onClick={() => navigate('/user/chats')}>
                        <ListItemPrefix>
                            <ChatBubbleBottomCenterTextIcon className="h-5 w-5" />
                        </ListItemPrefix>
                        Chats
                        <ListItemSuffix>
                            <Chip value="14" size="sm" variant="ghost" color="blue-gray" className="rounded-full" />
                        </ListItemSuffix>
                    </ListItem>
                    <ListItem>
                        <ListItemPrefix>
                            <UserCircleIcon className="h-5 w-5" />
                        </ListItemPrefix>
                        Perfil
                    </ListItem>
                    <ListItem>
                        <ListItemPrefix>
                            <Cog6ToothIcon className="h-5 w-5" />
                        </ListItemPrefix>
                        Configuración
                    </ListItem>
                </List>
                <List className="flex-1 justify-end">
                    <ListItem>
                        <ListItemPrefix>
                            <PowerIcon className="h-5 w-5" />
                        </ListItemPrefix>
                        Cerrar Sesión
                    </ListItem>
                </List>
            </Card>
        </div>
    );
}

export default SideBar;