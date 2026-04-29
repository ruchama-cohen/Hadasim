using DL.Models;
using System;

namespace BLL.Utils
{
    public static class MapUtil
    {
        private static readonly CoordinateSystem TelAviv = new CoordinateSystem
        {
            Latitude = new LocationDetail { Degrees = "32", Minutes = "5", Seconds = "7" },
            Longitude = new LocationDetail { Degrees = "34", Minutes = "46", Seconds = "54" }
        };

        public static CoordinateSystem ReturningLocationForTheTeacher()
        {
            return RandomLocationLottery(TelAviv, 1000);
        }

        public static CoordinateSystem CalculateDynamicLocation(CoordinateSystem originalCoords, double metersToMove)
        {
            if (originalCoords == null) return null;

            double latBase = ConvertToDecimalNumber(originalCoords.Latitude);
            double lonBase = ConvertToDecimalNumber(originalCoords.Longitude);

            double latNow = AddMetersToLat(latBase, metersToMove);
            double lonNow = AddMetersToLon(lonBase, latBase, metersToMove);

            return new CoordinateSystem
            {
                Latitude = ConvertDecimalToCoordinate(latNow),
                Longitude = ConvertDecimalToCoordinate(lonNow)
            };
        }

        public static double ConvertToDecimalNumber(LocationDetail location)
        {
            if (location == null || string.IsNullOrEmpty(location.Degrees)) return 0;

            try
            {
                double degrees = double.Parse(location.Degrees);
                double minutes = double.Parse(location.Minutes);
                double seconds = double.Parse(location.Seconds);
                return degrees + (minutes / 60.0) + (seconds / 3600.0);
            }
            catch
            {
                return 0;
            }
        }

        public static LocationDetail ConvertDecimalToCoordinate(double decimalValue)
        {
            double number = Math.Abs(decimalValue);
            double degrees = Math.Floor(number);
            double minutesFull = (number - degrees) * 60.0;
            double minutes = Math.Floor(minutesFull);
            double seconds = Math.Round((minutesFull - minutes) * 60.0, 2);

            return new LocationDetail
            {
                Degrees = degrees.ToString(),
                Minutes = minutes.ToString(),
                Seconds = seconds.ToString()
            };
        }

        public static double AddMetersToLat(double lat, double meters) => lat + (meters / 111320.0);

        public static double AddMetersToLon(double lon, double lat, double meters) =>
            lon + (meters / (111320.0 * Math.Cos(lat * Math.PI / 180.0)));

        public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = (lat2 - lat1) * 111320.0;
            double dLon = (lon2 - lon1) * 111320.0 * Math.Cos(lat1 * Math.PI / 180.0);
            return Math.Sqrt(dLat * dLat + dLon * dLon) / 1000.0;
        }

        public static CoordinateSystem RandomLocationLottery(CoordinateSystem center, int maxMeters)
        {
            Random rand = new Random();
            double randomOffsetLat = (rand.NextDouble() * 2 - 1) * maxMeters;
            double randomOffsetLon = (rand.NextDouble() * 2 - 1) * maxMeters;

            double baseLat = ConvertToDecimalNumber(center.Latitude);
            double baseLon = ConvertToDecimalNumber(center.Longitude);

            double newLat = AddMetersToLat(baseLat, randomOffsetLat);
            double newLon = AddMetersToLon(baseLon, baseLat, randomOffsetLon);

            return new CoordinateSystem
            {
                Latitude = ConvertDecimalToCoordinate(newLat),
                Longitude = ConvertDecimalToCoordinate(newLon)
            };
        }
    }
}