using ITHSManagement.Data;
using ITHSManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ITHSManagement.Services
{
    public class GroupService : Service<Group>
    {
        private readonly IDbContextFactory<EFContext> dbContextFactory;

        public GroupService(IDbContextFactory<EFContext> dbContextFactory) : base(dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
    }
}
