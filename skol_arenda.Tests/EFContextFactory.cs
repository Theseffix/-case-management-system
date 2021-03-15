using System.Collections.Generic;
using System.Linq;
using ITHSManagement.Data;
using ITHSManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace skol_arenda.Tests
{
    class EFContextFactory : IDbContextFactory<EFContext>
    {
        public EFContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<EFContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=skolarendetest;Trusted_Connection=True;");
            var context = new EFContext(builder.Options);
            //Create database.
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return Seed(context);
        }

        private EFContext Seed(EFContext context)
        {
            context.Course.Add(new Course { CourseName = "Tom_Kurs" });
            context.SaveChanges();
            context.Student.Add(new Student { FirstName = "Tom_Student" });
            context.Programmes.Add(new Programme { ProgramName = "Tom_Utbildning" });
            context.CompRep.Add(new CompRep { FirstName = "Tom_CompRep" });
            context.Group.Add(new Group { Title = "Tom_Grupp" });

            var student = new Student { FirstName = "Student" };
            var utbildning = new Programme { ProgramName = "Utbildning" };
            var grupp = new Group { Title = "Grupp" };
            var comprep = new CompRep { FirstName = "CompRep" };
            var course = new Course { CourseName = "Kurs" };

            context.Student.Add(student);
            context.SaveChanges();
            context.Programmes.Add(utbildning);
            context.Course.Add(course);
            context.CompRep.Add(comprep);
            context.Group.Add(grupp);
            context.SaveChanges();

            utbildning.Students = new List<Student>();
            utbildning.Students.Add(student);
            course.Students = new List<Student>();
            grupp.Members = new List<Person>();
            course.Students.Add(student);
            grupp.Members.Add(student);

            context.SaveChanges();

            return context;
        }
    }

}
