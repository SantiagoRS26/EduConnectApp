import React, { useEffect, useRef, useState } from "react";
import { MapContainer, TileLayer, Popup, Marker, useMapEvents } from "react-leaflet";
import { icon } from "leaflet";
import collegesController from "../services/api/collegesController";
import { AcademicCapIcon } from "@heroicons/react/24/outline";
import "../../node_modules/leaflet/dist/leaflet.css";
import { Button } from "@material-tailwind/react";


const Map = ({ onCollegeSelect, dataUser }) => {
    const [loading, setLoading] = useState(true);
    const [colleges, setColleges] = useState(null);
    const [selectedLocation, setSelectedLocation] = useState(null);
    const [selectedCollege, setSelectedCollege] = useState(null);


    const markerIcon = icon({
        iconUrl: require("../assets/img/map/iconMarker.png"),
        iconSize: [40, 40],
        iconAnchor: [20, 40],
        popupAnchor: [0, -40],
    });

    const markerIconUser = icon({
        iconUrl: require("../assets/img/map/userMarker.png"),
        iconSize: [40, 40],
        iconAnchor: [20, 40],
        popupAnchor: [0, -40],
    });

    useEffect(() => {
        const getColleges = async () => {
            try {
                const collegesData = await collegesController.colleges();
                // Aquí puedes guardar las ubicaciones en el estado o hacer cualquier otra manipulación necesaria
                setColleges(collegesData);
                setLoading(false);
            } catch (error) {
                console.error('Error al obtener las ubicaciones:', error);
            }
        };

        getColleges();
    }, []);


    if (loading) {
        return <h1>Cargando</h1>;
    }

    return (
        <div>
            <MapContainer
                center={[4.144042445752373, -73.63421055733126]}
                zoom={14}
                style={{ height: '600px' }}
                className="rounded-md"

            >
                <TileLayer
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                />

                {colleges.map((college, index) => (
                    <Marker
                        key={index}
                        position={[college.latitude, college.longitude]}
                        icon={dataUser && dataUser.collegeId==college.collegeId ? (markerIconUser) : (college==selectedCollege ? markerIconUser : markerIcon)}
                        eventHandlers={{
                            click: () => {
                                setSelectedCollege(college);
                                onCollegeSelect(college);
                            }
                        }}
                    >
                        <Popup>
                            <strong>{college.name}</strong>
                            <br />
                            {college.address}
                            <br />
                            Información adicional: {college.additionalInfo}
                            <br />
                            Disponibilidad: {college.availableSlots}
                        </Popup>
                    </Marker>
                ))}
            </MapContainer>
        </div>


    );
};

export default Map;
