using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITHSManagement.Data;

namespace ITHSManagement.Models
{
    public class TaskCommentRepository : Repository<TaskComment>, ITaskCommentRepository
    {
        public TaskCommentRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        { }

        public List<TaskComment> GetAllTaskCommentsByTaskId(int TaskId)
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.TaskComments.Where(x => x.TaskID == TaskId).ToList();
           
            return q1;
        }

        public override async Task<int> Insert(TaskComment comment)
        {
            using var context = DbFactory.CreateDbContext();
            
            context.Add(comment);

            await context.SaveChangesAsync();

            return 1;
        }



        public List<TaskComment> GetAllTasks()
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.TaskComments.ToList();
            return q1;
        }
    }
}
