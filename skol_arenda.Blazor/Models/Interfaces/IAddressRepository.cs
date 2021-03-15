using System.Threading.Tasks;

namespace ITHSManagement.Models
{
    public interface IAddressRepository
    {
        Task<int> Insert(Address address);
    }
}