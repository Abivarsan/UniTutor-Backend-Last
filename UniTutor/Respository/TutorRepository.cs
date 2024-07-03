using UniTutor.Models;
using UniTutor.Interface;
using UniTutor.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniTutor.Repository
{
    public class TutorRepository : ITutor
    {
        private readonly ApplicationDbContext _context;

        public TutorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tutor>> GetAllTutorsAsync()
        {
            return await _context.Tutors.ToListAsync();
        }

        public async Task AddTutorAsync(Tutor tutor)
        {
            await _context.Tutors.AddAsync(tutor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTutorAsync(Tutor tutor)
        {
            _context.Tutors.Update(tutor);
            await _context.SaveChangesAsync();
        }
    }
}
