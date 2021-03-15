using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ITHSManagement.Data;
using System.Linq;
using System.Collections.Generic;

namespace ITHSManagement.Models
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {

        public AddressRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        {
        }
        public override async Task<int> Insert(Address address)
        {
            using var context = DbFactory.CreateDbContext();

            context.Add(address);

            return await context.SaveChangesAsync();
        }
        public List<Address> GetAllAddressesById(int UserId)
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.Addresses.Where(x => x.UserId == UserId).ToList();

            return q1;
        }
    }
}
