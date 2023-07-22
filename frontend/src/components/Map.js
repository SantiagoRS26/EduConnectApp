import React, { useEffect, useRef, useState, useContext } from "react";
import { MapContainer, TileLayer, Popup, Marker, useMapEvents } from "react-leaflet";
import { icon } from "leaflet";
import collegesController from "../services/api/collegesController";
import { AcademicCapIcon } from "@heroicons/react/24/outline";
import "../../node_modules/leaflet/dist/leaflet.css";
import { Button } from "@material-tailwind/react";
import { UserContext } from "../routes/UserRoutes";

const Map = ({ onCollegeSelect = () => { }, collegeDataSelected = null }) => {

    const [collegesData, setColleges] = useState(null);
    const [selectedCollege, setSelectedCollege] = useState(null);
    const markerRefs = useRef([]);
    const { userData, colleges } = useContext(UserContext);

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
      if(collegeDataSelected == null) return
      markerRefs.current[collegeDataSelected.collegeId].openPopup();
    }, [collegeDataSelected]);

    return (
        <div className="flex-grow">
            <MapContainer
                center={[4.144042445752373, -73.63421055733126]}
                zoom={14}
                className="rounded-md h-full"

            >
                <TileLayer
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                />

                {colleges && colleges.map((college, index) => (
                    <Marker
                        key={index}
                        isSelected={college.collegeId == "f84900d0-ffd2-4f54-8062-0f40d95900c5"}
                        position={[college.latitude, college.longitude]}
                        icon={userData && userData.college && userData.college.id == college.collegeId ? (markerIconUser) : (markerIcon)}
                        eventHandlers={{
                            click: () => {
                                setSelectedCollege(college);
                                onCollegeSelect(college);
                            }
                        }}
                        ref={(ref) => {
                            markerRefs.current[college.collegeId] = ref;
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
