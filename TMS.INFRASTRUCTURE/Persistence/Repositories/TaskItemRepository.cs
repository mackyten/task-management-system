using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Enums;
using TMS.DOMAIN.Interfaces;

namespace TMS.INFRASTRUCTURE.Persistence.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext dbContext;

        public TaskItemRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            dbContext.Tasks.Add(task);
            await dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await dbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }
            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<TaskItem?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItem>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskItem> UpdateAsync(TaskItem task)
        {
            var existingTask = await dbContext.Tasks.FindAsync(task.Id);
            if (existingTask == null)
            {
                throw new Exception("Task not found");
            }
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate?.ToUniversalTime();
            existingTask.Priority = task.Priority;
            existingTask.AssignedToId = task.AssignedToId;
            existingTask.AssignedTo = task.AssignedTo;
            existingTask.DateFinished = task.DateFinished;
            await dbContext.SaveChangesAsync();
            return task;
        }
    }
}