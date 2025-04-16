using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.Enums;

namespace TMS.DOMAIN.Entities
{
    public class TaskItem : BaseEntity<int>
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public TaskItemStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public string? AssignedToId { get; set; }
        public ApplicationUser? AssignedTo { get; set; }
        public DateTime? DateFinished { get; set; } 
    }
}


// How these tables can be used for a Dashboard:

// Task Counts by Status: Query TaskItems grouped by Status (joining with TaskStatuses for names).
// Task Counts by Priority: Query TaskItems grouped by Priority (joining with TaskPriorities for names).
// Tasks Assigned to Users: Join TaskItems with AspNetUsers on AssignedToId to display tasks per user.
// Overdue Tasks: Filter TaskItems where DueDate is in the past and Status is not "Completed" or "Cancelled" (you'll need to know the Id or Name for these statuses).
// Task Creation Trends: Query TaskItems grouped by creation date (CreatedAt).
// Time Tracking: Analyze EstimatedTime vs. ActualTimeSpent.
// Project-based Views: If you implement the Projects table, you can filter and group tasks by ProjectId.
// Tag-based Views: If you implement the Tags table, you can filter tasks by specific tags.
