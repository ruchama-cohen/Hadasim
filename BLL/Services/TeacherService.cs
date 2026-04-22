using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.API;
using BLL.DTOs;
using DL.API;
using DL.Models;

namespace BLL.Services
{
    public class TeacherService: ITeacherService
    {
        private readonly ITeacherRepository _TeacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            _TeacherRepository = teacherRepository;
        }
       

        public async Task<TeacherDTO> GetTeacherByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("ID is required");
            }

            var teacherFromDb = await _TeacherRepository.GetTeacherById(id);

            if (teacherFromDb == null)
            {
                return null!;
            }

            return new TeacherDTO
            {
                TId = teacherFromDb.TId,
                LastName = teacherFromDb.FirstName,
                FirstName = teacherFromDb.LastName,
                ClassName = teacherFromDb.ClassName
            };
        }

        public async Task RegisterTeacherAsync(TeacherDTO teacher)
        {
            if (string.IsNullOrEmpty(teacher.TId) || teacher.TId.Length != 9)
            {
                throw new Exception("תעודת זהות חייבת להכיל 9 ספרות בדיוק");
            }
            var teacherNew = new Teacher
            {
                TId = teacher.TId,
                ClassName = teacher.ClassName,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName
            };

            await _TeacherRepository.AddTeacherAsync(teacherNew);



        }
    }
}
