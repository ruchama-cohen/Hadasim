using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.API;
using DL.Models;

namespace DL.API
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentById(string id);

        Task AddStudentAsync(Student student);

        Task<List<Student>> GetByClassAsync(string className);
    }
}


