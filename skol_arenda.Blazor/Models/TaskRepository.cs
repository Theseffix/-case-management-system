using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITHSManagement.Data;
using System;

namespace ITHSManagement.Models
{
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        { }

        public override async Task<int> Delete(TaskItem taskItem)
        {
            using var context = DbFactory.CreateDbContext();

            context.Remove(taskItem);

            await context.SaveChangesAsync();

            return 1;
        }
        public List<TaskItem> GetAllTasksById(int userId)
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.Tasks.Where(x => x.Person.Id == userId).ToList();
           
            return q1;
        }

        public List<TaskItem> GetAllTasksByProgrammeId(int progID)
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.Tasks.Where(x => x.ProgrammeId == progID).ToList();

            return q1;
        }

        public List<TaskItem> GetAllTasksByCourseId(int courseID)
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.Tasks.Where(x => x.CourseId == courseID).ToList();

            return q1;
        }
        
        public List<TaskItem> GetAllTasksByGroupId(int groupID)
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.Tasks.Where(x => x.GroupId == groupID).ToList();

            return q1;
        }

        public override async Task<int> Insert(TaskItem taskItem)
        {
            using var context = DbFactory.CreateDbContext();

            context.Add(taskItem);

            await context.SaveChangesAsync();

            return 1;
        }



        public List<TaskItem> GetAllTasks()
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.Tasks.ToList();
            return q1;
        }

        public override async Task<int> Update(TaskItem taskItem)
        {
            using var context = DbFactory.CreateDbContext();

            var q1 = context.Tasks.SingleOrDefault(x => x.Id == taskItem.Id);

            DateTime DefaultDate = new DateTime();

            if(taskItem.UpdateCount != 0)
            {
            q1.UpdateCount = taskItem.UpdateCount;
            }
            if (taskItem.Title != null)
            {
                q1.Title = taskItem.Title;
            }
            if (taskItem.Description != null)
            {
                q1.Description = taskItem.Description;
            }
            if (taskItem.StartDate != DefaultDate)
            {
                q1.StartDate = taskItem.StartDate;
            }
            if (taskItem.EndDate != DefaultDate)
            {
                q1.EndDate = taskItem.EndDate;
            }
            if (taskItem.Status != null)
            {
                q1.Status = taskItem.Status;
            }
            if (!(taskItem.Consequence < 1) && !(taskItem.Consequence > 5))
            {
                q1.Consequence = taskItem.Consequence;
            }
            if (!(taskItem.Priority < 1) && !(taskItem.Priority > 5))
            {
                q1.Priority = taskItem.Priority;
            }

            await context.SaveChangesAsync();

            return 1;
        }

        public Task<string> GetOwnerName(TaskItem taskItem)
        {
            
            using var context = DbFactory.CreateDbContext();

            string ownerName = "";

            if (taskItem.PersonId != null)
            {
                var student = context.Student.SingleOrDefault(x => x.Id == taskItem.PersonId);
                ownerName = student.FirstName + " " + student.LastName;
            }

            if (taskItem.ProgrammeId != null)
            {
                var program = context.Programmes.SingleOrDefault(x => x.Id == taskItem.ProgrammeId);
                ownerName = program.ProgramName;
            }

            if (taskItem.CourseId != null)
            {
                var course = context.Course.SingleOrDefault(x => x.Id == taskItem.CourseId);
                ownerName = course.CourseName;
            }

            //TODO: Add all entity Types to Linq Quereys.
            //var group = context.Group.SingleOrDefault(x => x.Id == personID);

            return Task.FromResult(ownerName);
        }
    }
}
