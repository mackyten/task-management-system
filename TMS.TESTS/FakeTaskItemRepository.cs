using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Interfaces;

namespace TMS.TESTS;

/// <summary>In-memory ITaskItemRepository so handlers can be tested without a database.</summary>
public class FakeTaskItemRepository : ITaskItemRepository
{
    private readonly Dictionary<int, TaskItem> items = new();
    private int nextId = 1;

    public bool ThrowOnAdd { get; set; }

    public Task<TaskItem> AddAsync(TaskItem task)
    {
        if (ThrowOnAdd)
            throw new InvalidOperationException("database unavailable");

        task.Id = nextId++;
        items[task.Id] = task;
        return Task.FromResult(task);
    }

    public Task<TaskItem?> GetById(int id) =>
        Task.FromResult(items.TryGetValue(id, out var task) ? task : null);

    public Task<IEnumerable<TaskItem>> GetPaginatedAsync(int pageNumber, int pageSize) =>
        Task.FromResult(items.Values.Skip((pageNumber - 1) * pageSize).Take(pageSize));

    public Task<TaskItem> UpdateAsync(TaskItem task)
    {
        items[task.Id] = task;
        return Task.FromResult(task);
    }

    public Task<bool> DeleteAsync(int id) => Task.FromResult(items.Remove(id));
}
