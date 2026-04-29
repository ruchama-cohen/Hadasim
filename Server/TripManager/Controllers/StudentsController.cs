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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("monitor/{teacherId}")]
        public async Task<IActionResult> GetMonitor(string teacherId)
        {
            var data = await _studentService.GetStudentsMonitoringAsync(teacherId);
            return Ok(data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] StudentDTO student)
        {
            try
            {
                await _studentService.RegisterStudentAsync(student);
                return Ok(new { message = "התלמידה נרשמה בהצלחה" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-location")]
        public async Task<IActionResult> UpdateLocation([FromBody] LocationUpdateDTO updateData)
        {
            try
            {
                await _studentService.UpdateStudentLocationAsync(updateData);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}