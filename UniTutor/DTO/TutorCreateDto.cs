namespace UniTutor.DTO
{
    public class TutorCreateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool Verified { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
