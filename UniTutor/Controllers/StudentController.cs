using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniTutor.DTO;
using UniTutor.Interface;
using UniTutor.Models;
using UniTutor.Repository;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _studentRepository;

        public StudentController(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("GetStudentbyId/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentDto = new StudentGetDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                AvatarUrl = student.AvatarUrl,
                Email = student.Email,
                Phone = student.Phone
            };

            return Ok(studentDto);
        }

        [HttpGet("GetStudents")]
        public async Task<ActionResult<IEnumerable<StudentGetDto>>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            var studentDtos = new List<StudentGetDto>();

            foreach (var student in students)
            {
                studentDtos.Add(new StudentGetDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    AvatarUrl = student.AvatarUrl,
                    Email = student.Email,
                    Phone = student.Phone
                });
            }

            return Ok(studentDtos);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] StudentCreateDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest();
            }

            TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time"); // Change to your local time zone
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localZone);

            var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                AvatarUrl = studentDto.AvatarUrl,
                Email = studentDto.Email,
                Phone = studentDto.Phone
            };

            await _studentRepository.AddStudentAsync(student);
            return Ok();
        }


        [HttpPost("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentRepository.DeleteStudentAsync(id);
            return Ok();
        }
    }
}
