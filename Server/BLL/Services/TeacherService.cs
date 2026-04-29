using BLL.API;
using BLL.DTOs;
using BLL.Utils;
using DL.API;
using DL.Models;
using System;
using System.Threading.Tasks;
using static BLL.Exceptions.ExceptionsForStudebtAndTeacher;

namespace BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private static readonly DateTime _tripStartTime = DateTime.UtcNow;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<bool> TeacherExistsAsync(string id)
        {
            var teacher = await _teacherRepository.GetTeacherById(id);
            return teacher != null;
        }

        public async Task RegisterTeacherAsync(TeacherDTO teacherDto)
        {
            if (string.IsNullOrEmpty(teacherDto.TId) || teacherDto.TId.Length != 9 || !teacherDto.TId.All(char.IsDigit))
            {
                throw new InvalidIdException(teacherDto.TId);
            }
            var existingTeacherById = await _teacherRepository.GetTeacherById(teacherDto.TId);
            if (existingTeacherById != null)
            {
                throw new TeacherAlreadyExistsException( teacherDto.TId);
            }

            var allTeachers = await _teacherRepository.GetAllTeachers();
            var existingTeacherInClass = allTeachers.FirstOrDefault(t => t.ClassName == teacherDto.ClassName);
            if (existingTeacherInClass != null)
            {
                throw new TeacherClassExistsException(teacherDto.ClassName);
            }

            var initialCoordinates = MapUtil.ReturningLocationForTheTeacher();

            var teacher = new Teacher
            {
                TId = teacherDto.TId,
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                ClassName = teacherDto.ClassName,
                Coordinates = initialCoordinates,
                LastUpdate = DateTime.UtcNow
            };
            await _teacherRepository.AddTeacherAsync(teacher);
        }

        public async Task<TeacherDTO> GetTeacherByIdAsync(string id)
        {
            var teacher = await _teacherRepository.GetTeacherById(id);
            if (teacher == null) return null!;

            double minutesPassed = (DateTime.UtcNow - _tripStartTime).TotalMinutes;
            double metersToMove = minutesPassed * 50.0;

            double lat = MapUtil.ConvertToDecimalNumber(teacher.Coordinates.Latitude);
            double lon = MapUtil.ConvertToDecimalNumber(teacher.Coordinates.Longitude);

            return new TeacherDTO
            {
                TId = teacher.TId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                ClassName = teacher.ClassName,
                Coordinates = new CoordinateSystem
                {
                    Latitude = MapUtil.ConvertDecimalToCoordinate(MapUtil.AddMetersToLat(lat, metersToMove)),
                    Longitude = MapUtil.ConvertDecimalToCoordinate(MapUtil.AddMetersToLon(lon, lat, metersToMove))
                },
                LastUpdate = teacher.LastUpdate
            };
        }
    }
}