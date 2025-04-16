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
        Task<TaskItem?> GetById(int id);
        Task<IEnumerable<TaskItem>> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<TaskItem> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
    }
}