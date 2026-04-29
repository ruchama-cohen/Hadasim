using BLL.DTOs;
using DL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.API
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetAllStudentsAsync();

        Task<StudentDTO> GetStudentByIdAsync(string id);

        Task RegisterStudentAsync(StudentDTO student);

        Task<List<StudentMonitorDTO>> GetStudentsMonitoringAsync(string teacherId);

        Task UpdateStudentLocationAsync(LocationUpdateDTO updateData);


        Task<List<StudentDTO>> GetStudentsByClassAsync(string className);
    }
}