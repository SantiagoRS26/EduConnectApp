import React, { useEffect, useState, useContext } from 'react';
import SideBar from '../../components/SiderBar';
import createSignalRConnection from '../../utils/SignalRClient';
import educationIllustration from '../../assets/img/illustrations/education.svg'
import { Card, Chip } from '@material-tailwind/react';
import requestController from '../../services/api/requestController';
import Map from '../../components/Map';
import { UserContext } from '../../routes/UserRoutes';

const Home = () => {
  const [requests, setRequests] = useState(null);
  const [collegeData, setCollegeData] = useState(null);
  const { userData } = useContext(UserContext);

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const formattedDate = `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
    return formattedDate;
  };



  const connection = createSignalRConnection();
  connection.start()
    .then(() => {
    })
    .catch((error) => {
      console.error("Error al establecer la conexión con el servidor SignalR", error);
    });

  useEffect(() => {
    const fetchRequest = async () => {
      try {
        const response = await requestController.getRequest();
        if (response) {
          setRequests(response);
        }
      } catch (error) {
        console.log(error);
      }
    }
    fetchRequest();
  }, []);

  return (
    <div className="flex w-full h-full overflow-hidden">
      <div className='flex-initial'>
        <SideBar />
      </div>

      <div className="flex-grow">
        <div className='w-full flex flex-col justify-center items-center mt-10 gap-y-6'>
          <div className='flex w-11/12 bg-[#fb7185]/40 px-24 py-16 rounded-3xl'>
            <div className='w-1/2 flex flex-col gap-y-5'>
              <h1 className='text-2xl text-[#fb7185] font-semibold'>Bienvenido de vuelta!</h1>
              <p className='text-lg'>Es un placer tenerte nuevamente aqui en nuestra plataforma</p>
            </div>
            <div className='w-1/2 rounded-xl flex items-center justify-center'>
              <img className='absolute h-56 top-3' src={educationIllustration} />
            </div>
          </div>
          <Card className='flex lg:flex-row lg:w-11/12 rounded-3xl mb-5 p-10 lg:gap-x-6 gap-y-6'>
            <Card className='p-10 w-full lg:w-1/2'>
              <h1 className='text-lg mb-5'>Resumen de solicitudes:</h1>
              <Card className='flex flex-row w-full'>
                <table className='w-full table-auto'>
                  <thead>
                    <tr>
                      <th className="border-b border-r border-blue-gray-200 p-4">Fecha</th>
                      <th className="border-b border-x border-blue-gray-200 p-4">Colegio Destino</th>
                      <th className="border-b border-l border-blue-gray-200 p-4">Estado</th>
                    </tr>
                  </thead>
                  <tbody>
                    {requests ? requests.map((request, index) => {
                      const isLast = index === requests.length - 1;
                      const classes = isLast ? "p-4" : "p-4 border-b border-blue-gray-50";

                      return (
                        <tr key={index} className={classes}>
                          <td className='text-center py-3 px-2'>
                            <p>{formatDate(request.createdDate)}</p>
                          </td>
                          <td className='text-center px-2'>
                            <p onClick={() => { setCollegeData(request.college)}} className='cursor-pointer underline hover:text-blue-400 duration-300'>{request.college.name}</p>
                          </td>
                          <td className='text-center px-2'>
                            <div className='w-full flex justify-center items-center'>
                              <Chip
                                size="sm"
                                variant="ghost"
                                value={request.status}
                                color={request.status === "En Proceso" ? "green" : "red"}
                              />
                            </div>

                          </td>
                        </tr>
                      );
                    }) : (<tr></tr>)}
                  </tbody>
                </table>
              </Card>
            </Card>
            <Card className='w-full lg:w-1/2 h-[400px]'>
              <Map dataUser={userData} collegeDataSelected={collegeData}/>

            </Card>
          </Card>
          <Card className='w-11/12'>
                    
          </Card>
        </div>
      </div>
    </div>
  );
};


export default Home;
