﻿using Microsoft.EntityFrameworkCore;
using UniTutor.Data;
using UniTutor.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniTutor.Respository
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetLastJoinedUsersAsync(int count)
        {
            var lastJoinedStudents = await _context.Students
                .OrderByDescending(s => s.CreatedAt)
                .Take(count)
                .Select(s => new { s.Id, FullName = s.FirstName + " " + s.LastName, s.Email, s.AvatarUrl, s.CreatedAt, Type = "Student" })
                .ToListAsync();

            var lastJoinedVerifiedTutors = await _context.Tutors
               .Where(t => t.Verified)  // Filter for only verified tutors
               .OrderByDescending(t => t.CreatedAt)
               .Take(count)
               .Select(t => new { t.Id, FullName = t.FirstName + " " + t.LastName, t.Email, t.AvatarUrl, t.CreatedAt, Type = "Tutor" })
               .ToListAsync();

            return lastJoinedStudents.Concat(lastJoinedVerifiedTutors)
                .OrderByDescending(u => u.CreatedAt)
                .Take(count)
                .ToList();
        }
    }
}
