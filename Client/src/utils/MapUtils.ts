import {  DMS } from '../types';
export const convertToDecimal = (coord?: DMS): number | null => {
    if (!coord) return null;
    const degrees = Number(coord.degrees);
    const minutes = Number(coord.minutes || 0);
    const seconds = Number(coord.seconds || 0);
    if (isNaN(degrees)) return null;
    let decimalResult = degrees + (minutes / 60) + (seconds / 3600);
    if (coord.direction === 'S' || coord.direction === 'W') {
        decimalResult = decimalResult * -1;
    }

    return decimalResult;
};