using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Models
{
    public class LocationDetail
    {
        public string Degrees { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }


    }

    public class CoordinateSystem
    {
        public LocationDetail Longitude { get; set; }
        public LocationDetail Latitude { get; set; }
    }
}
