using BLL.API;
using BLL.DTOs;
using BLL.Utils;
using DL.API;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BLL.Exceptions.ExceptionsForStudebtAndTeacher;

namespace BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        private static readonly DateTime _tripStartTime = DateTime.UtcNow;

        public StudentService(IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<List<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudents();

            double minutesPassed = (DateTime.UtcNow - _tripStartTime).TotalMinutes;
            double metersToMove = minutesPassed * 50.0;

            return students.Select(s => new StudentDTO
            {
                SId = s.SId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                ClassName = s.ClassName,
                Coordinates = MapUtil.CalculateDynamicLocation(s.Coordinates, metersToMove)
            }).ToList();
        }


        public async Task<List<StudentMonitorDTO>> GetStudentsMonitoringAsync(string teacherId)
        {
            var teacher = await _teacherRepository.GetTeacherById(teacherId);
            if (teacher == null) return new List<StudentMonitorDTO>();

            double minutesPassed = (DateTime.UtcNow - _tripStartTime).TotalMinutes;
            double metersToMove = minutesPassed * 50.0;

            var teacherCoordsNow = MapUtil.CalculateDynamicLocation(teacher.Coordinates, metersToMove);
            double tLatNow = MapUtil.ConvertToDecimalNumber(teacherCoordsNow.Latitude);
            double tLonNow = MapUtil.ConvertToDecimalNumber(teacherCoordsNow.Longitude);

            var allStudents = await _studentRepository.GetAllStudents();
            var myStudents = allStudents.Where(s => s.ClassName == teacher.ClassName && s.Coordinates != null);

            return myStudents.Select(s => {
                var sCoordsNow = MapUtil.CalculateDynamicLocation(s.Coordinates, metersToMove);
                double sLatNow = MapUtil.ConvertToDecimalNumber(sCoordsNow.Latitude);
                double sLonNow = MapUtil.ConvertToDecimalNumber(sCoordsNow.Longitude);

                double dist = MapUtil.GetDistance(tLatNow, tLonNow, sLatNow, sLonNow);

                return new StudentMonitorDTO
                {
                    SId = s.SId,
                    FullName = $"{s.FirstName} {s.LastName}",
                    DecimalLatitude = sLatNow,
                    DecimalLongitude = sLonNow,
                    DistanceFromTeacher = Math.Round(dist, 2),
                    IsFar = dist > 3.0,
                    IsActive = (DateTime.UtcNow - s.LastUpdate).TotalMinutes < 10,
                    LastUpdateStr = s.LastUpdate.ToLocalTime().ToString("HH:mm")
                };
            }).ToList();
        }

        public async Task RegisterStudentAsync(StudentDTO student)
        {
            if (string.IsNullOrEmpty(student.SId) || student.SId.Length != 9 || !student.SId.All(char.IsDigit))
            {
                throw new InvalidIdException(student.SId);
            }

            var existingStudent = await _studentRepository.GetStudentById(student.SId);
            if (existingStudent != null)
            {
                throw new StudentAlreadyExistsException(student.SId);
            }

            var teachers = await _teacherRepository.GetAllTeachers();
            var teacher = teachers.FirstOrDefault(t => t.ClassName == student.ClassName);

            if (teacher == null)
            {
                throw new TeacherNotFoundException(student.ClassName);
            }

            CoordinateSystem location = MapUtil.RandomLocationLottery(teacher.Coordinates, 100);

            var studentNew = new Student
            {
                SId = student.SId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ClassName = student.ClassName,
                Coordinates = location,
                LastUpdate = DateTime.UtcNow
            };

            await _studentRepository.AddStudentAsync(studentNew);
        }

        public async Task<StudentDTO> GetStudentByIdAsync(string id)
        {
            var s = await _studentRepository.GetStudentById(id);
            if (s == null) return null!;
            return new StudentDTO { SId = s.SId, FirstName = s.FirstName, LastName = s.LastName, ClassName = s.ClassName, Coordinates = s.Coordinates };
        }

        public async Task<List<StudentDTO>> GetStudentsByClassAsync(string className)
        {
            var students = await _studentRepository.GetAllStudents();
            return students
                .Where(s => s.ClassName == className)
                .Select(s => new StudentDTO
                {
                    SId = s.SId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    ClassName = s.ClassName,
                    Coordinates = s.Coordinates
                }).ToList();
        }


        public async Task UpdateStudentLocationAsync(LocationUpdateDTO updateData)
        {
            // כרגע הפונקציה הזו ריקה כי אנחנו משתמשים בסימולציה, 
            // אבל היא חייבת להופיע כאן כדי שה-Interface יהיה מרוצה.
            await Task.CompletedTask;
        }
    }
}