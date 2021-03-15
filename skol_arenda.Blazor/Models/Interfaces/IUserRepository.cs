using System.Threading.Tasks;

namespace ITHSManagement.Models
{
    public interface IUserRepository : IRepository<Person>
    {
        Task<int> AddContactInfoToUser(ContactInfo contactInfo, int userId);
    }
}