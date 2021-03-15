using ITHSManagement.Data;
using ITHSManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITHSManagement.Services
{
    public class CourseService : Service<Course>
    {
        private IDbContextFactory<EFContext> dbContextFactory;
        public CourseService(IDbContextFactory<EFContext> dbContextFactory) : base(dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<List<Programme>> SearchProgramByName(string name, int courseId)
        {
            var context = dbContextFactory.CreateDbContext();
            var course = context.Course.Find(courseId);

            var programs = from programmes in context.Set<Programme>()
                .Where(c => c.ProgramName.ToLower().Contains(name))
                .Include(c => c.Courses).Where(p => !p.Courses.Contains(course))
                          select programmes;

            return await programs.ToListAsync();
        }


        public async Task<List<Student>> SearchStudentByName(string name, int courseId)
        {
            var context = dbContextFactory.CreateDbContext();
            var students = await context.Student
                .Where(s => s.FirstName.ToLower().Contains(name) || s.LastName.ToLower().Contains(name))
                .ToListAsync();
            var usersInCourse = context.Course.Where(p => p.Id == courseId).Include(k => k.Students).FirstOrDefault().Students;
            var studentsInCourse = context.Student.Where(x => usersInCourse.Contains(x));
            return students.Except(studentsInCourse).ToList();

        }
        public async Task<List<CompRep>> SearchCompRepByName(string name, int courseId)
        {
            var context = dbContextFactory.CreateDbContext();
            var compRep = await context.CompRep
                .Where(s => s.FirstName.ToLower().Contains(name) || s.LastName.ToLower().Contains(name))
                .ToListAsync();
            var usersInCourse = context.Course.Where(p => p.Id == courseId).Include(k => k.CompanyReps).FirstOrDefault().CompanyReps;
            var compRepsInCourse = context.CompRep.Where(x => usersInCourse.Contains(x));
            return compRep.Except(compRepsInCourse).ToList();

        }
        public async Task<List<Group>> SearchGroupsByName(string name, int courseId)
        {
            var context = dbContextFactory.CreateDbContext();
            var course = context.Course.Find(courseId);

            var groupsToReturn = from groups in context.Set<Group>()
                .Where(c => c.Title.ToLower().Contains(name))
                .Include(c => c.Courses).Where(p => !p.Courses.Contains(course))
                           select groups;

            return await groupsToReturn.ToListAsync();
        }
        public async Task<int> AddStudentsFromSearch(List<Student> students, int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var course = await context.Course.Where(x => x.Id == courseId)
                                                  .Include(m => m.Students)
                                                  .FirstOrDefaultAsync();
            course.Students = course.Students.Union(students).ToList();
            context.Update(course);
           
            return await context.SaveChangesAsync();

        }
        public async Task<int> AddProgramFromSearch(List<Programme> programmes, int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var course = await context.Course.Where(x => x.Id == courseId)
                .Include(p => p.Programmes)
                .FirstOrDefaultAsync();

            var ds = context.Programmes.Where(x => programmes.Select(c => c.Id).Contains(x.Id));
            course.Programmes = course.Programmes.Union(ds).ToList();
            context.Update(course);
            return await context.SaveChangesAsync();
        }
        public async Task<int> AddGroupFromSearch(List<Group> groups, int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var course = await context.Course.Where(x => x.Id == courseId)
                                                    .Include(c => c.Groups)
                                                    .FirstOrDefaultAsync();

            var ds = context.Group.Where(x => groups.Select(c => c.Id).Contains(x.Id));
            course.Groups = course.Groups.Union(ds).ToList();
            context.Update(course);
            return await context.SaveChangesAsync();
        }
        public async Task<int> AddCompRepsFromSearch(List<CompRep> compReps, int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var course = await context.Course.Where(x => x.Id == courseId)
                                                    .Include(c => c.CompanyReps)
                                                    .FirstOrDefaultAsync();

            var ds = context.CompRep.Where(x => compReps.Select(c => c.Id).Contains(x.Id));
            course.CompanyReps = course.CompanyReps.Union(ds).ToList();
            context.Update(course);
            return await context.SaveChangesAsync();
        }
        public async Task<List<Student>> GetStudentsByCourseId(int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();
            //Fetch programme users and filter out non students
            var students = context.Course
                .Where(x => x.Id == courseId)
                .Include(m => m.Students
                    .Where(u => u.UserType == UserTypeEnum.Student))
                .FirstOrDefault()
                .Students;

            //Fetch Students in previous list.
            var result = context.Student.Where(x => students.Contains(x)).ToList();

            return await Task.FromResult(result);
        }
        public async Task<List<CompRep>> GetCompanyRepsByCourseId(int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var course = await context.Course
                .Where(x => x.Id == courseId)
                .Include(m => m.CompanyReps).FirstOrDefaultAsync();

            return course.CompanyReps.ToList();
        }

        public async Task<List<Programme>> GetProgrammesByCourseId(int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var courses = context.Course
                .Include(x => x.Programmes)
                .Where(x => x.Id == courseId)
                .FirstOrDefault().Programmes.ToList();

            return await Task.FromResult(courses);
        }

        public async Task<List<Group>> GetGroupsByCourseId(int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var courses = context.Course
                .Include(x => x.Groups)
                .Where(x => x.Id == courseId)
                .FirstOrDefault().Groups.ToList();

            return await Task.FromResult(courses);
        }

        public async Task<int> RemoveStudentsFromCourse(List<Student> students, int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var course = await context.Course.Where(x => x.Id == courseId)
                                                  .Include(m => m.Students)
                                                  .FirstOrDefaultAsync();
            var userIds = students.Select(x => x.Id);
            var users = context.Student.Where(u => userIds.Contains(u.Id));
            course.Students = course.Students.Except(users).ToList();
            context.Update(course);
            return await context.SaveChangesAsync();
        }
        public async Task<int> RemoveCompRepsFromCourse(List<CompRep> compRep, int courseId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var course = await context.Course.Where(x => x.Id == courseId)
                                                  .Include(m => m.CompanyReps)
                                                  .FirstOrDefaultAsync();

            course.CompanyReps = course.CompanyReps
                    .Where(x => !compRep
                        .Select(y => y.Id)
                        .Contains(x.Id)).ToList();

            context.Update(course);
            return await context.SaveChangesAsync();
        }
        public async Task<bool> DeletedCourseFromProgram(int courseId, int programmeId)
        {
            var context = dbContextFactory.CreateDbContext();
            var course = context.Course.Include(x => x.Programmes).Where(x => x.Id == courseId).First();
            var programmeToBeRemoved = course.Programmes.Where(x => x.Id == programmeId).FirstOrDefault();
            bool result = course.Programmes.Remove(programmeToBeRemoved);

            context.Update(course);

            return result && await context.SaveChangesAsync() != 0;
        }
    }
}
