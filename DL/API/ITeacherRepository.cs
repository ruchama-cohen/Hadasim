using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Models;

namespace DL.API
{
    public interface ITeacherRepository
    {

        Task<List<Teacher>> GetAllTeachers();

        Task<Teacher> GetTeacherById(string id);

        Task AddTeacherAsync(Teacher teacher);
    }
}
