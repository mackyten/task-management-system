using System;
using System.Collections.Generic;
using System.Linq;
using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Enums;

namespace TMS.DOMAIN.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<TaskItem> AddAsync(TaskItem task);
        Task<IEnumerable<TaskItem>> AddManyAsync(IEnumerable<TaskItem> tasks);
        
        Task<TaskItem?> GetById(int id);
        Task<IEnumerable<TaskItem>> GetAll();
        Task<IEnumerable<TaskItem>> GetByStatusAsync(TaskStatus status);
        Task<IEnumerable<TaskItem>> GetByUserAsync(int userId);
        Task<IEnumerable<TaskItem>> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<TaskItem>> SearchAsync(string keyword);
        Task<IEnumerable<TaskItem>> GetOverdueTasksAsync();
        Task<IEnumerable<TaskItem>> GetByPriorityAsync(TaskPriority priority);
        Task<IEnumerable<TaskItem>> GetRecentTasksAsync(DateTime fromDate, DateTime toDate);

        Task<TaskItem> UpdateAsync(TaskItem task);
        Task<bool> UpdateManyAsync(IEnumerable<TaskItem> tasks);
        
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteManyAsync(IEnumerable<int> taskIds);
    }
}