using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.API;
using BLL.DTOs;
using DL.Models;

namespace BLL.API
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetStudentsByClassAsync(string className);
        Task<StudentDTO> GetStudentByIdAsync(string id);
        Task RegisterStudentAsync(StudentDTO student);
    }
}


