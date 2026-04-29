using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class StudentMonitorDTO : StudentDTO
    {
        public double DecimalLatitude { get; set; }
        public double DecimalLongitude { get; set; }
        public double DistanceFromTeacher { get; set; }
        public bool IsFar { get; set; }

        public string FullName { get; set; } 
        public string LastUpdateStr { get; set; } 
        public bool IsActive { get; set; } 
    }
}
