using Microsoft.AspNetCore.Mvc;
using UniTutor.Models;
using UniTutor.Repository;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly TutorRepository _tutorRepository;

        public TutorController(TutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        [HttpGet("tutors")]
        public async Task<IActionResult> GetTutors()
        {
            var tutors = await _tutorRepository.GetAllTutorsAsync();
            return Ok(tutors);
        }

        [HttpPost("addtutor")]
        public async Task<IActionResult> AddTutor([FromBody] Tutor tutor)
        {
            await _tutorRepository.AddTutorAsync(tutor);
            return Ok();
        }

        
    }
}
