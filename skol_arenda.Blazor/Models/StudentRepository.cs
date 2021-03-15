using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITHSManagement.Data;
using System.Linq;
using System;

namespace ITHSManagement.Models
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        {
        }
        public override async Task<int> Insert(Student student)
        {
            using var context = DbFactory.CreateDbContext();

            //student.User = new User
            //{
            //    UserType = UserTypeEnum.Student
            //};
            context.Add(student);

            //transaction.Commit();

            await context.SaveChangesAsync();
            context.Entry(student).GetDatabaseValues();
            return 1; //fix
        }

        public override async Task<int> Update(Student s)
        {
            using var context = DbFactory.CreateDbContext();

            var q1 = context.Student.SingleOrDefault(x => x.Id == s.Id);

            DateTime DefaultDate = new DateTime();

            if (s.FirstName != "")
            {
                q1.FirstName = s.FirstName;
            }
            if (s.LastName != "")
            {
                q1.LastName = s.LastName;
            }
            if (s.Birthdate != DefaultDate)
            {
                q1.Birthdate = s.Birthdate;
            }

            await context.SaveChangesAsync();

            return 1;
        }
    }
}
