using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITHSManagement.Data;
using ITHSManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ITHSManagement.Services
{
    public class StudentService : Service<Student>
    {
        private IDbContextFactory<EFContext> dbContextFactory;
        public StudentService(IDbContextFactory<EFContext> dbContextFactory) : base(dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<List<ContactInfo>> GetContactInfoFromStudent(int Id)
        {
            var context = dbContextFactory.CreateDbContext();

            var user = await context.Persons.Include(x => x.ContactInfos).Where(x => x.Id == Id).FirstOrDefaultAsync();
            return user.ContactInfos.ToList();
        }
    }
}
