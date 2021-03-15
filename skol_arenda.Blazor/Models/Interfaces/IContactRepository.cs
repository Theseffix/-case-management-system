using System.Threading.Tasks;

namespace ITHSManagement.Models
{
    public interface IContactRepository
    {
        Task<int> Insert(ContactInfo contact);
    }
}