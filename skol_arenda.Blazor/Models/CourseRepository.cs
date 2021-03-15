using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITHSManagement.Data;
using System.Linq;
using System;

namespace ITHSManagement.Models
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        {
        }
        public override async Task<int> Insert(Course course)
        {
            using var context = DbFactory.CreateDbContext();

            context.Add(course);

            await context.SaveChangesAsync();
            context.Entry(course).GetDatabaseValues();
            return 1; //fix
        }

        public override async Task<int> Update(Course c)
        {
            using var context = DbFactory.CreateDbContext();

            var q1 = context.Course.SingleOrDefault(x => x.Id == c.Id);

            DateTime DefaultDate = new DateTime();

            if(c.CourseName != "")
            {
                q1.CourseName = c.CourseName;
            }
            if (c.Description != "")
            {
                q1.Description = c.Description;
            }
            if (c.StartDate != DefaultDate)
            {
                q1.StartDate = c.StartDate;
            }
            if (c.EndDate != DefaultDate)
            {
                q1.EndDate = c.EndDate;
            }
            if (c.Points != null)
            {
                q1.Points = c.Points;
            }
            if (c.CourseAdmin != "")
            {
                q1.CourseAdmin = c.CourseAdmin;
            }

            await context.SaveChangesAsync();

            return 1;
        }
    }
}
