using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITHSManagement.Data;
using System.Linq;
using System;

namespace ITHSManagement.Models
{
    public class ProgrammeRepository : Repository<Programme>, IProgrammeRepository
    {
        public ProgrammeRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        {
        }
        public override async Task<int> Insert(Programme programme)
        {
            using var context = DbFactory.CreateDbContext();

            context.Add(programme);

            await context.SaveChangesAsync();
            context.Entry(programme).GetDatabaseValues();
            return 1; //fix
        }

        public override async Task<int> Update(Programme p)
        {
            using var context = DbFactory.CreateDbContext();

            var q1 = context.Programmes.SingleOrDefault(x => x.Id == p.Id);

            DateTime DefaultDate = new DateTime();

            if (p.Description != "")
            {
                q1.Description = p.Description;
            }
            if (p.StartDate != DefaultDate)
            {
                q1.StartDate = p.StartDate;
            }
            if (p.EndDate != DefaultDate)
            {
                q1.EndDate = p.EndDate;
            }
            if (p.YhNumber != null)
            {
                q1.YhNumber = p.YhNumber;
            }
            if (p.CourseAdmin != "")
            {
                q1.CourseAdmin = p.CourseAdmin;
            }

            await context.SaveChangesAsync();

            return 1;
        }
    }
}
