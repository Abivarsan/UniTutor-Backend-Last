using System.ComponentModel.DataAnnotations.Schema;

namespace UniTutor.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }

        // Foreign key to Student or Tutor
        public int? StudentId { get; set; }
        public int? TutorId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("TutorId")]
        public virtual Tutor Tutor { get; set; }

        [NotMapped]
        public string FullName => UserType == "Student" ? $"{Student.FirstName} {Student.LastName}" : $"{Tutor.FirstName} {Tutor.LastName}";
    }
}
