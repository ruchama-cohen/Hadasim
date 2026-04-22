using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.API;
using BLL.DTOs;
using DL.Models;
using DL.API;

namespace BLL.Services
{

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentDTO>> GetStudentsByClassAsync(string className)
        {
            var studentsFromDb = await _studentRepository.GetByClassAsync(className);

            if (className == null || !className.Any())
            {
                throw new Exception("Class not found");
            }

            var studentDtos = studentsFromDb.Select(s => new StudentDTO
            {
                SId = s.SId,
                LastName = s.FirstName,
                FirstName = s.LastName,
                ClassName = s.ClassName
            }).ToList();

            return studentDtos;
        }

        public async Task<StudentDTO> GetStudentByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("ID is required");
            }

            var studentFromDb = await _studentRepository.GetStudentById(id);

            if (studentFromDb == null)
            {
                return null!;
            }

            return new StudentDTO
            {
                SId = studentFromDb.SId,
                LastName = studentFromDb.FirstName,
                FirstName = studentFromDb.LastName,
                ClassName = studentFromDb.ClassName
            };
        }

        public async Task RegisterStudentAsync(StudentDTO student)
        {
            if (string.IsNullOrEmpty(student.SId) || student.SId.Length != 9)
            {
                throw new Exception("תעודת זהות חייבת להכיל 9 ספרות בדיוק");
            }
            var studentNew = new Student
            {
                SId = student.SId,
                ClassName = student.ClassName,
                FirstName = student.FirstName,
                LastName = student.LastName
            };

            await _studentRepository.AddStudentAsync(studentNew);



        }


    }
}
