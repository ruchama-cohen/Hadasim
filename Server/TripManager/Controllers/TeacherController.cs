using BLL.API;
using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public TeachersController(ITeacherService teacherService, IStudentService studentService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetTeacherById(string id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null) return NotFound("מורה לא נמצאה.");
            return Ok(teacher);
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> CheckExists(string id)
        {
            var exists = await _teacherService.TeacherExistsAsync(id);
            return Ok(exists);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] TeacherDTO teacherDto)
        {
            try
            {
                await _teacherService.RegisterTeacherAsync(teacherDto);
                return Ok(new { message = "המורה נרשמה בהצלחה" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{teacherId}/students")]
        public async Task<IActionResult> GetStudentsByTeacherClass(string teacherId)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(teacherId);
            if (teacher == null) return NotFound("מורה לא נמצאה.");

            var students = await _studentService.GetStudentsByClassAsync(teacher.ClassName);
            return Ok(students);
        }
    }
}