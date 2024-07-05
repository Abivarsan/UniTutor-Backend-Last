using UniTutor.Models;
using System.Collections.Generic;

namespace UniTutor.Interface
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task DeleteStudentAsync(int id);

    }
}
