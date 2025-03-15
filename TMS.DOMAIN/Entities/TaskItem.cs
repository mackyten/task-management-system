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
    }
}