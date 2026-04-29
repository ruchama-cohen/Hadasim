using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Models;

namespace BLL.DTOs
{
    public class TeacherDTO
    {
        public string TId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClassName { get; set; }
        public CoordinateSystem? Coordinates { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
