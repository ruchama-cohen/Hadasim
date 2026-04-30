import React, { useEffect, useMemo } from 'react';
import { MapContainer, TileLayer, Marker, Popup, Tooltip, useMap } from 'react-leaflet';
import { Card, Badge, Stack } from 'react-bootstrap'; // ייבוא רכיבי בוסטראפ
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';
import { MapProps } from '../../types';
import { greenIcon, redIcon, teacherIcon } from './MapIcons';
import { convertToDecimal } from '../../utils/MapUtils';

const MapAutoResizer: React.FC<{ points: [number, number][] }> = ({ points }) => {
    const mapInstance = useMap();
    useEffect(() => {
        if (!points || points.length === 0) return;
        if (points.length === 1) {
            mapInstance.setView(points[0], 14);
        } else {
            mapInstance.fitBounds(L.latLngBounds(points), { padding: [50, 50], maxZoom: 15 });
        }
    }, [points, mapInstance]);
    return null;
};

const Map: React.FC<MapProps> = ({ students, teacher }) => {
    const teacherLat = convertToDecimal(teacher?.coordinates?.latitude);
    const teacherLng = convertToDecimal(teacher?.coordinates?.longitude);
    
    const allActivePositions = useMemo<[number, number][]>(() => {
        const results: [number, number][] = [];
        if (teacherLat !== null && teacherLng !== null) results.push([teacherLat, teacherLng]);
        students.forEach((s) => {
            const lat = s.decimalLatitude ?? convertToDecimal(s.coordinates?.latitude);
            const lng = s.decimalLongitude ?? convertToDecimal(s.coordinates?.longitude);
            if (lat !== null && lng !== null) results.push([lat, lng]);
        });
        return results;
    }, [students, teacherLat, teacherLng]);

    return (
        <Card className="shadow-lg border-0 rounded-4 overflow-hidden p-2 bg-light">
            <Card.Body className="p-0">
                <MapContainer 
                    center={teacherLat && teacherLng ? [teacherLat, teacherLng] : [31.7683, 35.2137]} 
                    zoom={13} 
                    style={{ height: '600px', width: '100%', borderRadius: '12px' }}
                >
                    <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                    
                    {teacherLat && teacherLng && (
                        <Marker position={[teacherLat, teacherLng]} icon={teacherIcon}>
                            <Tooltip permanent direction="top" offset={[0, -20]} className="bootstrap-id-badge bg-primary text-white border-0 shadow-sm">
                                {teacher?.tId}
                            </Tooltip>
                            <Popup>
                                <div className="text-end p-1" style={{ direction: 'rtl' }}>
                                    <h6 className="fw-bold text-primary mb-1">מורה: {teacher?.firstName} {teacher?.lastName}</h6>
                                    <div className="small text-muted">כיתה: {teacher?.className}</div>
                                    <Badge bg="info" className="mt-2 text-dark">מיקום מרכזי</Badge>
                                </div>
                            </Popup>
                        </Marker>
                    )}

                    {students.map((s, i) => {
                        const lat = s.decimalLatitude ?? convertToDecimal(s.coordinates?.latitude);
                        const lng = s.decimalLongitude ?? convertToDecimal(s.coordinates?.longitude);
                        if (lat === null || lng === null) return null;
                        return (
                            <Marker key={s.sId || i} position={[lat, lng]} icon={s.isFar ? redIcon : greenIcon}>
                                <Tooltip permanent direction="top" offset={[0, -20]} className={`bootstrap-id-badge border-0 shadow-sm ${s.isFar ? 'bg-danger' : 'bg-success'} text-white`}>
                                    {s.sId}
                                </Tooltip>
                                <Popup>
                                    <div className="text-end p-1" style={{ direction: 'rtl' }}>
                                        <h6 className="fw-bold mb-1">{s.firstName} {s.lastName}</h6>
                                        <Stack direction="horizontal" gap={2} className="mb-2">
                                            <Badge bg="secondary" style={{ fontSize: '0.75rem' }}>כיתה: {s.className}</Badge>                                            <Badge bg={s.isFar ? 'danger' : 'success'} pill>
                                                {s.isFar ? 'מרוחק' : 'תקין'}
                                            </Badge>
                                        </Stack>
                                        
                                        {s.distanceFromTeacher && (
                                            <div className={`p-2 rounded-2 small fw-bold border ${s.isFar ? 'bg-danger-subtle text-danger border-danger' : 'bg-success-subtle text-success border-success'}`}>
                                                מרחק: {Number(s.distanceFromTeacher).toFixed(2)} ק"מ
                                            </div>
                                        )}
                                    </div>
                                </Popup>
                            </Marker>
                        );
                    })}
                    <MapAutoResizer points={allActivePositions} />
                </MapContainer>
            </Card.Body>

            <style>{`
                .bootstrap-id-badge {
                    padding: 2px 6px !important;
                    font-size: 10px !important;
                    font-weight: 600 !important;
                    border-radius: 4px !important;
                    opacity: 0.9;
                }

                .leaflet-popup-content-wrapper { 
                    border-radius: 15px !important; 
                    box-shadow: 0 4px 15px rgba(0,0,0,0.1) !important;
                }
                
                .leaflet-popup-close-button { 
                    right: auto !important; 
                    left: 8px !important; 
                    top: 8px !important;
                }

                .leaflet-tooltip-top:before {
                    border-top-color: transparent !important;
                }
            `}</style>
        </Card>
    );
};

export default Map;