using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.API;


using BLL.Services;
using DL.Services;

namespace TripManager.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public TeacherController(ITeacherService teacherService, IStudentService studentService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<TeacherDTO>> GetTeacherById(string id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);
                if (teacher == null)
                    return NotFound("התלמידה לא נמצאה במערכת");

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{teacherId}/students")]
        public async Task<ActionResult<List<StudentDTO>>> GetTecherByTeacherClass(string teacherId)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(teacherId);
                if (teacher == null) return NotFound("המורה לא נמצאה");
                var students = await _studentService.GetStudentsByClassAsync(teacher.ClassName);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> RegisterStudent([FromBody] TeacherDTO teacher)
        {
            try
            {
                if (teacher == null)
                    return BadRequest("נתוני מורה לא תקינים");

                await _teacherService.RegisterTeacherAsync(teacher);
                return Ok(new { message = "התלמידה נרשמה בהצלחה" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}
