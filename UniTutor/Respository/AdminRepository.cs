//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UniTutor.Data;
//using UniTutor.Interface;
//using UniTutor.Models;

//namespace UniTutor.Repository
//{
//    public class AdminRepository : IAdmin
//    {
//        private readonly ApplicationDbContext _context;

//        public AdminRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }


//        public async Task<IEnumerable<Tutor>> GetAllTutorsAsync()
//        {
//            // Implement logic to fetch all tutors
//            return await _context.Tutors.Where(a=>a.Verified==false).ToListAsync();
//        }

//        public async Task<Tutor> GetTutorByIdAsync(int id)
//        {
//            // Example implementation, ensure it handles null case
//            var tutor = await _context.Tutors.FindAsync(id);
//            return tutor ?? throw new Exception($"Tutor with id {id} not found");
//        }

//        public async Task AcceptTutorAsync(int id)
//        {
//            // Implement logic to accept a tutor
//            var tutor = await _context.Tutors.FindAsync(id);
//            if (tutor != null)
//            {
//                tutor.Verified = true; // Example: Update tutor status
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task RejectTutorAsync(int id)
//        {
//            // Implement logic to reject a tutor
//            var tutor = await _context.Tutors.FindAsync(id);
//            if (tutor != null)
//            {
//                tutor.Verified = false; // Example: Update tutor status
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniTutor.Data;
using UniTutor.Interface;
using UniTutor.Models;

namespace UniTutor.Repository
{
    public class AdminRepository : IAdmin
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tutor>> GetAllTutorsAsync()
        {
            // Fetch all tutors who are not yet verified
            return await _context.Tutors.Where(a => a.Verified == false).ToListAsync();
        }

        public async Task<Tutor> GetTutorByIdAsync(int id)
        {
            var tutor = await _context.Tutors.FindAsync(id);
            return tutor ?? throw new Exception($"Tutor with id {id} not found");
        }

        public async Task AcceptTutorAsync(int id)
        {
            var tutor = await _context.Tutors.FindAsync(id);
            if (tutor != null)
            {
                tutor.Verified = true; // Update tutor status to verified
                await _context.SaveChangesAsync();
            }
        }

        public async Task RejectTutorAsync(int id)
        {
            var tutor = await _context.Tutors.FindAsync(id);
            if (tutor != null)
            {
                _context.Tutors.Remove(tutor); // Delete tutor from database
                await _context.SaveChangesAsync();
            }
        }
    }
}
