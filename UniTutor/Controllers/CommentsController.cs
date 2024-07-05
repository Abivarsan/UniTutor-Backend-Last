
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniTutor.Data;
using UniTutor.DTO;
using UniTutor.Models;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentGetDto>>> GetComments()
        {
            var comments = await _context.Comments
                .Include(c => c.Student)
                .Include(c => c.Tutor)
                .Select(c => new CommentGetDto
                {
                    Id = c.Id,
                    UserType = c.UserType,
                    FullName = c.UserType == "Student" ? $"{c.Student.FirstName} {c.Student.LastName}" :
                               c.UserType == "Tutor" ? $"{c.Tutor.FirstName} {c.Tutor.LastName}" : "",
                    CommentText = c.CommentText,
                    Date = c.Date
                })
                .ToListAsync();

            return comments;
        }

        [HttpPost]
        public async Task<ActionResult<CommentGetDto>> PostComment(CommentCreateDto commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest();
            }

            Comment comment = new Comment
            {
                UserType = commentDto.UserType,
                CommentText = commentDto.CommentText,
                Date = commentDto.Date
            };

            if (commentDto.UserType == "Student")
            {
                comment.StudentId = commentDto.UserId; // Assuming you have UserId in CommentCreateDto
            }
            else if (commentDto.UserType == "Tutor")
            {
                comment.TutorId = commentDto.UserId; // Assuming you have UserId in CommentCreateDto
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var fullName = string.Empty;
            if (comment.UserType == "Student")
            {
                var student = await _context.Students.FindAsync(comment.StudentId);
                fullName = $"{student.FirstName} {student.LastName}";
            }
            else if (comment.UserType == "Tutor")
            {
                var tutor = await _context.Tutors.FindAsync(comment.TutorId);
                fullName = $"{tutor.FirstName} {tutor.LastName}";
            }

            var commentGetDto = new CommentGetDto
            {
                Id = comment.Id,
                UserType = comment.UserType,
                FullName = fullName,
                CommentText = comment.CommentText,
                Date = comment.Date
            };

            return CreatedAtAction(nameof(GetComments), new { id = comment.Id }, commentGetDto);
        }
    }
}
