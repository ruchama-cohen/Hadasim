export interface Teacher {
    tId: string;
    firstName: string;
    lastName: string;
    className: string;
    coordinates?: {
        latitude: { degrees: string, minutes: string, seconds: string };
        longitude: { degrees: string, minutes: string, seconds: string };
    };
}

export interface Student {
    sId: string;
    firstName: string;
    lastName: string;
    className: string;
    isFar?: boolean;
    distanceFromTeacher?: string;
    decimalLatitude?: number;
    decimalLongitude?: number;
    coordinates?: {
        latitude: { degrees: string, minutes: string, seconds: string };
        longitude: { degrees: string, minutes: string, seconds: string };
    };
}

export interface MapProps {
    students: Student[];
    teacher: Teacher;
}

export type DMS = {
    degrees?: number | string;
    minutes?: number | string;
    seconds?: number | string;
    direction?: 'N' | 'S' | 'E' | 'W';
};