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

        public async Task<Tutor> GetTutorByIdAsync(int id)
        {
            return await _context.Tutors.FindAsync(id);
        }

        public async Task AddTutorAsync(Tutor tutor)
        {
            _context.Tutors.Add(tutor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTutorAsync(Tutor tutor)
        {
            _context.Entry(tutor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTutorAsync(int id)
        {
            var tutor = await _context.Tutors.FindAsync(id);
            if (tutor != null)
            {
                _context.Tutors.Remove(tutor);
                await _context.SaveChangesAsync();
            }
        }



        // Implement other methods as needed
    }
}
