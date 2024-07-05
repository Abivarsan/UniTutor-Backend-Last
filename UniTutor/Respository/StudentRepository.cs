using static UniTutor.Respository.StudentRepository;
using System;
using UniTutor.Models;
using UniTutor.Interface;
using UniTutor.Data;
using Microsoft.EntityFrameworkCore;

namespace UniTutor.Respository
{
        public class StudentRepository : IStudent
        {
            private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
            {
                _context = context;
            }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

       


        // Implement other methods as needed
    }
}

