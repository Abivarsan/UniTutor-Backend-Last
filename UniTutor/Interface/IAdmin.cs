using UniTutor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniTutor.Interface
{
    public interface IAdmin
    {
        Task<IEnumerable<Tutor>> GetAllTutorsAsync();
        Task<Tutor> GetTutorByIdAsync(int id);
        Task AcceptTutorAsync(int id);
        Task RejectTutorAsync(int id);
    }
}
