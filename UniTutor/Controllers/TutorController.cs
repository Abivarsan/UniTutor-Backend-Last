//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UniTutor.DTO;
//using UniTutor.Interface;
//using UniTutor.Models;
//using UniTutor.Repository;

//namespace UniTutor.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TutorController : ControllerBase
//    {
//        private readonly ITutor _tutorRepository;

//        public TutorController(ITutor tutorRepository)
//        {
//            _tutorRepository = tutorRepository;
//        }

//        [HttpGet("tutors")]
//        public async Task<ActionResult<IEnumerable<TutorGetDto>>> GetTutors()
//        {
//            var tutors = await _tutorRepository.GetAllTutorsAsync();
//            var tutorDtos = new List<TutorGetDto>();

//            foreach (var tutor in tutors)
//            {
//                tutorDtos.Add(new TutorGetDto
//                {
//                    Id = tutor.Id,
//                    FirstName = tutor.FirstName,
//                    LastName = tutor.LastName,
//                    AvatarUrl = tutor.AvatarUrl,
//                    Email = tutor.Email,
//                    Phone = tutor.Phone
//                });
//            }

//            return Ok(tutorDtos);
//        }

//        [HttpPost("addtutor")]
//        public async Task<IActionResult> AddTutor([FromBody] TutorCreateDto tutorDto)
//        {
//            if (tutorDto == null)
//            {
//                return BadRequest();
//            }

//            var tutor = new Tutor
//            {
//                FirstName = tutorDto.FirstName,
//                LastName = tutorDto.LastName,
//                AvatarUrl = tutorDto.AvatarUrl,
//                Email = tutorDto.Email,
//                Phone = tutorDto.Phone
//            };

//            await _tutorRepository.AddTutorAsync(tutor);
//            return Ok();
//        }



//        [HttpPost("deletetutor")]
//        public async Task<IActionResult> DeleteTutor(int id)
//        {
//            await _tutorRepository.DeleteTutorAsync(id);
//            return Ok();
//        }

//        [HttpGet("tutor/{id}")]
//        public async Task<IActionResult> GetTutor(int id)
//        {
//            var tutor = await _tutorRepository.GetTutorByIdAsync(id);
//            if (tutor == null)
//            {
//                return NotFound();
//            }

//            var tutorDto = new TutorGetDto
//            {
//                Id = tutor.Id,
//                FirstName = tutor.FirstName,
//                LastName = tutor.LastName,
//                AvatarUrl = tutor.AvatarUrl,
//                Email = tutor.Email,
//                Phone = tutor.Phone
//            };

//            return Ok(tutorDto);
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniTutor.DTO;
using UniTutor.Interface;
using UniTutor.Models;
using Microsoft.Extensions.Logging;
using System;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly ITutor _tutorRepository;
        private readonly ILogger<TutorController> _logger;

        public TutorController(ITutor tutorRepository, ILogger<TutorController> logger)
        {
            _tutorRepository = tutorRepository;
            _logger = logger;
        }

        [HttpGet("tutors")]
        public async Task<ActionResult<IEnumerable<TutorGetDto>>> GetTutors()
        {
            var tutors = await _tutorRepository.GetAllTutorsAsync();
            var tutorDtos = new List<TutorGetDto>();

            foreach (var tutor in tutors)
            {
                tutorDtos.Add(new TutorGetDto
                {
                    Id = tutor.Id,
                    FirstName = tutor.FirstName,
                    LastName = tutor.LastName,
                    AvatarUrl = tutor.AvatarUrl,
                    Email = tutor.Email,
                    Phone = tutor.Phone
                });
            }

            return Ok(tutorDtos);
        }

        [HttpPost("addtutor")]
        public async Task<IActionResult> AddTutor([FromBody] TutorCreateDto tutorDto)
        {
            if (tutorDto == null)
            {
                return BadRequest();
            }

            // Set CreatedAt to local time
            TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time"); // Change to your local time zone
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localZone);

            var tutor = new Tutor
            {
                FirstName = tutorDto.FirstName,
                LastName = tutorDto.LastName,
                AvatarUrl = tutorDto.AvatarUrl,
                Email = tutorDto.Email,
                Phone = tutorDto.Phone,
                Verified = tutorDto.Verified,
                CreatedAt = localDateTime
            };

            await _tutorRepository.AddTutorAsync(tutor);
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

            var tutorDto = new TutorGetDto
            {
                Id = tutor.Id,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                AvatarUrl = tutor.AvatarUrl,
                Email = tutor.Email,
                Phone = tutor.Phone
            };

            return Ok(tutorDto);
        }
    }
}
