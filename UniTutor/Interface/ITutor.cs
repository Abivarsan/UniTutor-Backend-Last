using UniTutor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniTutor.Interface
{
    public interface ITutor
    {
        Task<IEnumerable<Tutor>> GetAllTutorsAsync();
        Task<Tutor> GetTutorByIdAsync(int id);
        Task AddTutorAsync(Tutor tutor);
        Task UpdateTutorAsync(Tutor tutor);
        Task DeleteTutorAsync(int id);
    }
}
