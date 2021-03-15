using ITHSManagement.Models;
using ITHSManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ITHSManagement.Data
{
    public interface IEFContext
    {
        DbSet<Company> Company { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<CompRep> CompRep { get; set; }
        DbSet<ContactInfo> ContactInfo { get; set; }
        DbSet<Course> Course { get; set; }
        DbSet<Group> Group { get; set; }
        DbSet<LIAWork> LIAWork { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Programme> Programmes { get; set; }
        DbSet<Student> Student { get; set; }
        DbSet<TaskComment> TaskComments { get; set; }
        DbSet<TaskItem> Tasks { get; set; }
        DbSet<Todo> Todos { get; set; }

    }
}