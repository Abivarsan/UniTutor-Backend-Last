
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniTutor.Interface;
using UniTutor.Models;
using UniTutor.Respository;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _studentRepository;

        public  StudentController(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("GetStudentbyId{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            await _studentRepository.AddStudentAsync(student);
            return Ok();
        }

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            await _studentRepository.UpdateStudentAsync(student);
            return Ok();
        }

        [HttpPost("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentRepository.DeleteStudentAsync(id);
            return Ok();
        }
        
    }

}
