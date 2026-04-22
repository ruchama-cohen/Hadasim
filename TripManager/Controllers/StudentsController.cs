using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.API; 

namespace TripManager.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("class/{className}")]
        public async Task<ActionResult<List<StudentDTO>>> GetStudentsByClass(string className)
        {
            try
            {
                var students = await _studentService.GetStudentsByClassAsync(className);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudentById(string id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                    return NotFound("התלמידה לא נמצאה במערכת");

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> RegisterStudent([FromBody] StudentDTO student)
        {
            try
            {
                if (student == null)
                    return BadRequest("נתוני תלמידה לא תקינים");

                await _studentService.RegisterStudentAsync(student);
                return Ok(new { message = "התלמידה נרשמה בהצלחה" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

