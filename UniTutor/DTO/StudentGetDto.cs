namespace UniTutor.DTO
{
    public class StudentGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
