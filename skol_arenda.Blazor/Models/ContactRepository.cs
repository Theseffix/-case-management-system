using Microsoft.EntityFrameworkCore;
using ITHSManagement.Data;
using System.Threading.Tasks;

namespace ITHSManagement.Models
{
    public class ContactRepository : Repository<ContactInfo>, IContactRepository
    {

        public ContactRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        {
        }
        public override async Task<int> Insert(ContactInfo contact)
        {
            using var context = DbFactory.CreateDbContext();
            if(contact.Contact != "" && contact.Contact != null)
            {

            context.Add(contact);

            }
            return await context.SaveChangesAsync();
        }

        public override async Task<int> Delete(ContactInfo contact)
        {
            using var context = DbFactory.CreateDbContext();

            context.Remove(contact);

            await context.SaveChangesAsync();

            return 1;
        }

    }
}
