//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using UniTutor.Data;
//using UniTutor.Models;

//namespace UniTutor.Repositories
//{
//    public class RequestRepository : IRequest
//    {
//        private readonly ApplicationDbContext _context;

//        public RequestRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<Request>> GetWeeklyRequestsAsync()
//        {
//            DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
//            return await _context.Requests
//                .Where(r => r.Date >= startOfWeek)
//                .ToListAsync();
//        }
//    }
//}
