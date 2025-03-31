using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.Enums;

namespace TMS.APPLICATION.DTOs
{
    public class TaskItemResponseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public TaskItemStatus Status { get; set; }
    }
}