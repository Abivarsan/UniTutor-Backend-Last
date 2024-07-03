using Microsoft.AspNetCore.Mvc;
using UniTutor.Interface;
using UniTutor.Models;
using UniTutor.Repository;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly ITutor _tutorRepository;

        public TutorController(ITutor tutorRepository)
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

        [HttpPost("updatetutor")]
        public async Task<IActionResult> UpdateTutor([FromBody] Tutor tutor)
        {
            await _tutorRepository.UpdateTutorAsync(tutor);
            return Ok();
        }

        [HttpPost("deletetutor")]
        public async Task<IActionResult> DeleteTutor(int id)
        {
            await _tutorRepository.DeleteTutorAsync(id);
            return Ok();
        }

        [HttpGet("tutor/{id}")]
        public async Task<IActionResult> GetTutor(int id)
        {
            var tutor = await _tutorRepository.GetTutorByIdAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            return Ok(tutor);
        }


        
    }
}
