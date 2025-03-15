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

        public Task<IEnumerable<TaskItem>> AddManyAsync(IEnumerable<TaskItem> tasks)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteManyAsync(IEnumerable<int> taskIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskItem> GetAll()
        {
            throw new NotImplementedException();
        }



        public async Task<TaskItem?> GetById(int id)
        {
            var result = await dbContext.Tasks.FindAsync(id);
            return result;
        }

        public Task<IEnumerable<TaskItem>> GetByPriorityAsync(TaskPriority priority)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItem>> GetByStatusAsync(TaskStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItem>> GetByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItem>> GetOverdueTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItem>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItem>> GetRecentTasksAsync(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItem>> SearchAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<TaskItem> UpdateAsync(TaskItem task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateManyAsync(IEnumerable<TaskItem> tasks)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TaskItem>> ITaskItemRepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}