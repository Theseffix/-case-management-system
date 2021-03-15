using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ITHSManagement.Data;
namespace ITHSManagement.Models
{
    public class UserRepository : Repository<Person>, IUserRepository
    {
        public UserRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        { }

        public async Task<int> AddContactInfoToUser(ContactInfo contactInfo, int userId)
        {
            using var context = DbFactory.CreateDbContext();
            var user = context.Persons.Include(x => x.ContactInfos).Where(x => x.Id == userId).First();
            user.ContactInfos.Add(contactInfo);
            return await context.SaveChangesAsync();
        }
    }
}
