using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Models;

namespace BLL.DTOs
{
    public class LocationUpdateDTO
    {
        public string ID { get; set; } 
        public CoordinateSystem Coordinates { get; set; }
        public DateTime Time { get; set; }
    }
}
