using ITHSManagement.Models;
using ITHSManagement.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System;

namespace ITHSManagement.Services
{
    public class TaskService : Service<Task>
    {
        private IDbContextFactory<EFContext> dbContextFactory;
        public TaskService(IDbContextFactory<EFContext> dbContextFactory) : base(dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<bool> CreateTaskUser(TaskItem task, int userID)
        {
            using var context = dbContextFactory.CreateDbContext();
            var user = context.Persons.Where(x => x.Id == userID).Include(t => t.Tasks).FirstOrDefault();
            user.Tasks.Add(task);
            context.Add(task);

            context.Update(user);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateTaskProgramme(TaskItem task, int programID)
        {
            using var context = dbContextFactory.CreateDbContext();
            var prog = context.Programmes.Where(x => x.Id == programID).Include(t => t.Tasks).FirstOrDefault();
            prog.Tasks.Add(task);
            context.Add(task);

            context.Update(prog);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateTaskCourse(TaskItem task, int courseID)
        {
            using var context = dbContextFactory.CreateDbContext();
            var course = context.Course.Where(x => x.Id == courseID).Include(t => t.Tasks).FirstOrDefault();
            course.Tasks.Add(task);
            context.Add(task);

            context.Update(course);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateTaskGroup(TaskItem task, int groupID)
        {
            using var context = dbContextFactory.CreateDbContext();
            var group = context.Group.Where(x => x.Id == groupID).Include(t => t.Tasks).FirstOrDefault();
            group.Tasks.Add(task);
            context.Add(task);

            context.Update(group);

            await context.SaveChangesAsync();
            return true;
        }
    }
}
