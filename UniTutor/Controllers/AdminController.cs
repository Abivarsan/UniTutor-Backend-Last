using Microsoft.AspNetCore.Mvc;
using UniTutor.Interface;
using UniTutor.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UniTutor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _adminRepository;
        private readonly IEmailService _emailService;

        public AdminController(IAdmin adminRepository, IEmailService emailService)
        {
            _adminRepository = adminRepository;
            _emailService = emailService; // Correct initialization
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            // Implement logic to create a new admin
            return Ok();
        }

        [HttpGet("tutors")]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetTutors()
        {
            var tutors = await _adminRepository.GetAllTutorsAsync();
            return Ok(tutors);
        }

        [HttpPost("accepttutor/{id}")]
        public async Task<IActionResult> AcceptTutor(int id)
        {
            var tutor = await _adminRepository.GetTutorByIdAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }

            // Perform acceptance logic
            await _adminRepository.AcceptTutorAsync(id);

            // Send verification email
            var emailSubject = "Your tutor account has been accepted";
            var emailMessage = $"Dear {tutor.FirstName}, your tutor account has been accepted.";
            await _emailService.SendEmailAsync(tutor.Email, emailSubject, emailMessage);

            return Ok();
        }

        [HttpDelete("rejecttutor/{id}")]
        public async Task<IActionResult> RejectTutor(int id)
        {
            var tutor = await _adminRepository.GetTutorByIdAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }

            // Perform rejection logic
            await _adminRepository.RejectTutorAsync(id);

            // Send rejection email
            var emailSubject = "Your tutor account has been rejected";
            var emailMessage = $"Dear {tutor.FirstName}, unfortunately, your tutor account has been rejected.";
            await _emailService.SendEmailAsync(tutor.Email, emailSubject, emailMessage);

            return Ok();
        }
    }
}
