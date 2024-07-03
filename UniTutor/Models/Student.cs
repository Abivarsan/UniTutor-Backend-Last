using System.ComponentModel.DataAnnotations;

namespace UniTutor.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? Complaints { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Status { get; set; }
    }
}
