using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITHSManagement.Data;
using System.Linq;
using System;
using ITHSManagement.Models.Interfaces;

namespace ITHSManagement.Models
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        {
        }
        public override async Task<int> Insert(Group group)
        {
            using var context = DbFactory.CreateDbContext();

            context.Add(group);

            await context.SaveChangesAsync();
            context.Entry(group).GetDatabaseValues();
            return 1; //fix
        }

        public override async Task<int> Update(Group g)
        {
            using var context = DbFactory.CreateDbContext();

            var q1 = context.Group.SingleOrDefault(x => x.Id == g.Id);

            if(g.Title != "")
            {
                q1.Title = g.Title;
            }
            if (g.Description != "")
            {
                q1.Description = g.Description;
            }

            await context.SaveChangesAsync();

            return 1;
        }
    }
}
