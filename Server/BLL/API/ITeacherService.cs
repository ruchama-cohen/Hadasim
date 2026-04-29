using BLL.DTOs;
using System.Threading.Tasks;

namespace BLL.API
{
    public interface ITeacherService
    {
        Task<TeacherDTO> GetTeacherByIdAsync(string id);
        Task RegisterTeacherAsync(TeacherDTO teacherDto);
        Task<bool> TeacherExistsAsync(string id);
    }
}