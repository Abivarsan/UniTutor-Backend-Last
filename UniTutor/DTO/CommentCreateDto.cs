using UniTutor.Models;

namespace UniTutor.DTO
{
    public class CommentCreateDto
    {
        public string UserType { get; set; } // "Student" or "Tutor"
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; } // Assuming UserId is the ID of the Student or Tutor


    }
}
