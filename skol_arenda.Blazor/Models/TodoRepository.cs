using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITHSManagement.Data;
using System;

namespace ITHSManagement.Models
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(IDbContextFactory<EFContext> DbFactory)
            : base(DbFactory)
        { }

        public override async Task<int> Insert(Todo todoItem)
        {
            using var context = DbFactory.CreateDbContext();

            context.Add(todoItem);

            await context.SaveChangesAsync();

            return 1;
        }

        public List<Todo> GetAllTodosByTaskId(int taskID)
        {
            using var context = DbFactory.CreateDbContext();
            var q1 = context.Todos.Where(x => x.TaskID == taskID).ToList();

            return q1;
        }

        public override async Task<int> Delete(object todoObj)
        {
            using var context = DbFactory.CreateDbContext();

            context.Remove(todoObj);

            await context.SaveChangesAsync();

            return 1;
        }

        public override async Task<int> Update(Todo todoObj)
        {
            using var context = DbFactory.CreateDbContext();

            var q1 = context.Todos.SingleOrDefault(x => x.Id == todoObj.Id);

            DateTime DefaultDate = new DateTime();

            if (todoObj.Title != null)
            {
                q1.Title = todoObj.Title;
            }
            if (todoObj.Description != null)
            {
                q1.Description = todoObj.Description;
            }
            if (todoObj.StartDate != DefaultDate)
            {
                q1.StartDate = todoObj.StartDate;
            }
            if (todoObj.EndDate != DefaultDate)
            {
                q1.EndDate = todoObj.EndDate;
            }
            if (todoObj.Status != null)
            {
                q1.Status = todoObj.Status;
            }

            await context.SaveChangesAsync();

            return 1;
        }
    }
}
