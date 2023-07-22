import React, { useEffect, useState, useContext } from 'react';
import SideBar from '../../components/SiderBar';
import educationIllustration from '../../assets/img/illustrations/education.svg'
import { Button, Card, Chip, Dialog, DialogBody, DialogFooter, DialogHeader, Alert } from '@material-tailwind/react';
import requestController from '../../services/api/requestController';
import Map from '../../components/Map';
import { UserContext } from '../../routes/UserRoutes';
import { useForm } from 'react-hook-form';
import { ExclamationTriangleIcon } from "@heroicons/react/24/solid";

const Home = () => {
  const [requests, setRequests] = useState(null);
  const [collegeData, setCollegeData] = useState(null);
  const { userData } = useContext(UserContext);
  const [openDialogNewRequest, setOpenDialogNewRequest] = useState(false);
  const { register, handleSubmit, formState: { errors } } = useForm();
  const [selectedCollege, setSelectedCollege] = useState(null);
  const [errorForm, setErrorForm] = useState(null);
  const [alert, setAlert] = useState(false);

  const handleSelectedCollege = (college) => {
    setSelectedCollege(college);
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const formattedDate = `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
    return formattedDate;
  };

  const onSubmitForm = async () => {
    try {
      if (selectedCollege == null) {
        setErrorForm("No ha seleccionado ningun colegio");
        setAlert(true);
      }
      else {
        const data = {
          collegeId: selectedCollege.collegeId
        };
        const response = await requestController.sendRequest(data);
      }
    } catch (error) {
      setErrorForm(error.response.data);
      setAlert(true);
    }
  };

  const handleOpenDialogRequest = () => setOpenDialogNewRequest(!openDialogNewRequest);

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
          <Card className='flex flex-col w-11/12 rounded-3xl mb-5 p-10 gap-x-6 gap-y-6'>
            <div className='flex flex-col xl:flex-row'>
              <Card className='p-10 w-full xl:w-1/2'>
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
                      {requests && requests.length > 0 ? requests.map((request, index) => {
                        const isLast = index === requests.length - 1;
                        const classes = isLast ? "p-4" : "p-4 border-b border-blue-gray-50";
                        return (
                          <tr key={index} className={classes}>
                            <td className='text-center py-3 px-2'>
                              <p>{formatDate(request.createdDate)}</p>
                            </td>
                            <td className='text-center px-2'>
                              <p onClick={() => { setCollegeData(request.college) }} className='cursor-pointer underline hover:text-blue-400 duration-300'>{request.college.name}</p>
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
                      }) : (<tr>
                        <td colSpan="3" className='text-center py-3'>Actualmente no tiene solicitudes</td>
                      </tr>)}
                    </tbody>
                  </table>
                </Card>
              </Card>
              <Card className='w-full xl:w-1/2 h-[400px]'>
                <Map dataUser={userData} collegeDataSelected={collegeData} />
              </Card>
            </div>
            <div className='flex w-full justify-center items-center'>
              <Button onClick={handleOpenDialogRequest} className='w-1/5' color='green'>Nueva Solicitud</Button>
            </div>
            <Dialog open={openDialogNewRequest} handler={handleOpenDialogRequest} size='xl'>
              <Alert
                variant="gradient"
                className='absolute bottom-0 z-20 w-auto m-5 text-center'
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
                Error:
                {" " + errorForm}
              </Alert>
              <form onSubmit={handleSubmit(onSubmitForm)}>
                <DialogHeader className='text-center flex justify-center'>Explora y Encuentra el Colegio Ideal Para su Hijo y su Brillante Futuro</DialogHeader>
                <DialogBody >
                  <Card className='w-full h-[600px]'>
                    <Map onCollegeSelect={handleSelectedCollege} />
                  </Card>
                  <Card>
                    <h1 className='font-bold text-2xl text-center'>Colegio Seleccionado:</h1>
                    {selectedCollege && (
                      <div className='text-center'>
                        <p><span className='font-bold'>Nombre: </span> {selectedCollege.name}</p>
                        <p><span className='font-bold'>Información adicional: </span> {selectedCollege.additionalInfo}</p>
                        <p><span className='font-bold'>Direccion: </span> {selectedCollege.address}</p>
                      </div>)}
                  </Card>
                </DialogBody>
                <DialogFooter >
                  <Button
                    variant="text"
                    color="red"
                    onClick={() => handleOpenDialogRequest(null)}
                    className="mr-1"
                  >
                    <span>Cancel</span>
                  </Button>
                  <Button
                    variant="gradient"
                    color="green"
                    type='submit'
                  >
                    <span>Confirm</span>
                  </Button>
                </DialogFooter>
              </form>
            </Dialog>
          </Card>
        </div>
      </div>
    </div>
  );
};


export default Home;
