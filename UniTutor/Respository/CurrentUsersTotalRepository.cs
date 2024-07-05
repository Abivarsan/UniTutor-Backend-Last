using Microsoft.EntityFrameworkCore;
using UniTutor.Data;
using UniTutor.Interface;

namespace UniTutor.Respository
{
    public class CurrentUsersTotalRepository : ICurrentUsersTotal
    {
        private readonly ApplicationDbContext _context;

        public CurrentUsersTotalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalStudentsAsync()
        {
            return await _context.Students.CountAsync();
        }

        public async Task<int> GetTotalTutorsAsync()
        {
            return await _context.Tutors.CountAsync();
        }
    }
}
