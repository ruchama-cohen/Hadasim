using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.API
{
    public interface ITeacherService
    {
        Task<TeacherDTO> GetTeacherByIdAsync(string id);
        Task RegisterTeacherAsync(TeacherDTO teacher);
    }
}
