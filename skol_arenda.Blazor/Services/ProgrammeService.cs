using ITHSManagement.Models;
using ITHSManagement.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;

namespace ITHSManagement.Services
{
    public class ProgrammeService : Service<Programme>, IProgrammeService
    {
        private readonly IDbContextFactory<EFContext> dbContextFactory;
        private readonly ILogger<ProgrammeService> logger;
        public ProgrammeService(IDbContextFactory<EFContext> dbContextFactory, ILogger<ProgrammeService> logger) : base(dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
            this.logger = logger;

        }
        public async Task<int> InsertCompRep(CompRep compRep) // ENDAST FÖR TESTNING
        {

            using var context = dbContextFactory.CreateDbContext();

            //compRep.User = new User
            //{
            //    UserType = UserTypeEnum.Company
            //};

            context.Add(compRep);

            //transaction.Commit();

            await context.SaveChangesAsync();
            context.Entry(compRep).GetDatabaseValues();
            return 1; //fix
        }
        public async Task<bool> DeletedCourseFromProgram(int courseId, int programmeId)
        {
            var context = dbContextFactory.CreateDbContext();
            try
            {
                var course = context.Course.Include(x => x.Programmes).Where(x => x.Id == courseId).First();
                var programmeToBeRemoved = course.Programmes.Where(x => x.Id == programmeId).FirstOrDefault();
                bool result = course.Programmes.Remove(programmeToBeRemoved);
                context.Update(course);
                return result && await context.SaveChangesAsync() != 0;
            }
            catch (Exception e)
            {
                logger.LogError($"Error: DeletedCourseFromProgram: coursId: {courseId} programmeId: {programmeId} \n Exception: {e}");
                context.Dispose();
            }
            return false;

        }
        public async Task<bool> DeletedGroupFromProgram(int groupId, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var group = context.Group.Include(x => x.Programmes).Where(x => x.Id == groupId).First();
            var programmeToBeRemoved = group.Programmes.Where(x => x.Id == programmeId).FirstOrDefault();

            bool result = group.Programmes.Remove(programmeToBeRemoved);

            context.Update(group);

            return result && await context.SaveChangesAsync() != 0;
        }
        public async Task<List<Student>> GetStudentsByProgramId(int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            //Fetch programme users and filter out non students
            var program = await context.Programmes
                .Where(x => x.Id == programmeId)
                .Include(m => m.Students).FirstOrDefaultAsync();

            return program.Students.ToList();
        }
        public async Task<List<CompRep>> GetCompanyRepsByProgramId(int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var program = await context.Programmes
                .Where(x => x.Id == programmeId)
                .Include(m => m.CompanyReps).FirstOrDefaultAsync();

            return program.CompanyReps.ToList();
        }
        public async Task<bool> AddedStudentToProgram(Student student, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var program = await context.Programmes
                .Where(x => x.Id == programmeId)
                .Include(m => m.Students).FirstOrDefaultAsync();

            program.Students.ToList().Add(student);
            context.Update(program);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<int> RemoveStudentsFromProgram(List<Student> students, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var program = await context.Programmes.Where(x => x.Id == programmeId)
                                                  .Include(m => m.Students)
                                                  .FirstOrDefaultAsync();

            program.Students = program.Students
                    .Where(x => !students
                        .Select(y => y.Id)
                        .Contains(x.Id)).ToList();

            context.Update(program);
            return await context.SaveChangesAsync();
        }
        public async Task<int> RemoveCompRepsFromProgram(List<CompRep> compRep, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var program = await context.Programmes.Where(x => x.Id == programmeId)
                                                  .Include(m => m.CompanyReps)
                                                  .FirstOrDefaultAsync();

            program.CompanyReps = program.CompanyReps
                    .Where(x => !compRep
                        .Select(y => y.Id)
                        .Contains(x.Id)).ToList();

            context.Update(program);
            return await context.SaveChangesAsync();
        }
        public async Task<List<Student>> SearchStudentByName(string name, int programmeId)
        {
            var context = dbContextFactory.CreateDbContext();
            var students = await context.Student
                .Where(s => s.FirstName.ToLower().Contains(name) || s.LastName.ToLower().Contains(name))
                .ToListAsync();
            var studentsInProgram = context.Programmes.Where(p => p.Id == programmeId).Include(k => k.Students).FirstOrDefault().Students;
            return students.Except(studentsInProgram).ToList();

        }
        public async Task<List<CompRep>> SearchCompRepByName(string name)
        {
            var context = dbContextFactory.CreateDbContext();
            var compReps = context.CompRep
                .Where(s => s.FirstName.ToLower().Contains(name) || s.LastName.ToLower().Contains(name))
                .ToListAsync();

            return await compReps;
        }
        public async Task<List<Course>> SearchCoursesByName(string name, int programmeId)
        {
            var context = dbContextFactory.CreateDbContext();
            var program = context.Programmes.Find(programmeId);

            var courses = from course in context.Set<Course>()
                .Where(c => c.CourseName.ToLower().Contains(name))
                .Include(c => c.Programmes).Where(p => !p.Programmes.Contains(program))
                          select course;

            return await courses.ToListAsync();
        }
        public async Task<List<Group>> SearchGroupsByName(string name)
        {
            var context = dbContextFactory.CreateDbContext();
            var groups = context.Group
                .Where(s => s.Title.ToLower().Contains(name))
                .ToListAsync();

            return await groups;
        }
        public async Task<List<Course>> GetCoursesByProgramId(int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var courses = context.Programmes
                .Include(x => x.Courses)
                .Where(x => x.Id == programmeId)
                .FirstOrDefault().Courses.ToList();


            return await Task.FromResult(courses);
        }
        public async Task<List<Group>> GetGroupsByProgramId(int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var groups = context.Programmes
                .Include(x => x.Groups)
                .Where(x => x.Id == programmeId)
                .FirstOrDefault().Groups.ToList();

            return await Task.FromResult(groups);
        }
        public async Task<bool> DeleteGroupFromProgram(int groupId, int programmeId)
        {
            EFContext context = dbContextFactory.CreateDbContext();
            Group group = context.Group.Include(x => x.Programmes).Where(x => x.Id == groupId).First();
            Programme programmeToBeRemoved = group.Programmes.Where(x => x.Id == programmeId).FirstOrDefault();

            bool result = group.Programmes.Remove(programmeToBeRemoved);

            context.Update(group);

            return result && await context.SaveChangesAsync() != 0;
        }
        public async Task<bool> AddCourseToProgram(int courseId, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var course = context.Course.Include(x => x.Programmes).Where(x => x.Id == courseId).First();
            var program = await Get(programmeId);

            course.Programmes.Add(program);

            return context.SaveChanges() == 1;
        }
        public async Task<bool> AddGroupToProgram(int groupId, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var group = context.Group.Include(x => x.Programmes).Where(x => x.Id == groupId).First();

            var program = await Get(programmeId);
            group.Programmes.Add(program);

            return context.SaveChanges() == 1;
        }
        public async Task<int> AddStudentsFromSearch(List<Student> students, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var program = await context.Programmes.Where(x => x.Id == programmeId)
                                                  .Include(m => m.Students)
                                                  .FirstOrDefaultAsync();
            program.Students = program.Students.Union(students).ToList();
            context.Update(program);
            return await context.SaveChangesAsync();

        }
        public async Task<int> AddCoursesFromSearch(List<Course> courses, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var program = await context.Programmes.Where(x => x.Id == programmeId)
                                                    .Include(c => c.Courses)
                                                    .FirstOrDefaultAsync();

            var ds = context.Course.Where(x => courses.Select(c => c.Id).Contains(x.Id));
            program.Courses = program.Courses.Union(ds).ToList();
            context.Update(program);
            return await context.SaveChangesAsync();

        }
        public async Task<int> AddGroupsFromSearch(List<Group> groups, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var program = await context.Programmes.Where(x => x.Id == programmeId)
                                                    .Include(c => c.Groups)
                                                    .FirstOrDefaultAsync();

            var ds = context.Group.Where(x => groups.Select(c => c.Id).Contains(x.Id));
            program.Groups = program.Groups.Union(ds).ToList();
            context.Update(program);
            return await context.SaveChangesAsync();
        }
        public async Task<int> AddCompRepsFromSearch(List<CompRep> compReps, int programmeId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var program = await context.Programmes.Where(x => x.Id == programmeId)
                                                  .Include(m => m.CompanyReps)
                                                  .FirstOrDefaultAsync();

            program.CompanyReps = program.CompanyReps.Union(compReps).ToList();
            context.Update(program);
            return await context.SaveChangesAsync();
        }

    }
}
