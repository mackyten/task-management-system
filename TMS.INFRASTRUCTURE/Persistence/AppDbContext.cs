using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.DOMAIN.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TMS.INFRASTRUCTURE.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DbSet<TaskItem> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options,
                         IHttpContextAccessor httpContextAccessor)
         : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                foreach (var entry in ChangeTracker.Entries<TaskItem>())
                {
                    if (entry.State == EntityState.Added && string.IsNullOrEmpty(entry.Entity.CreatedById))
                    {
                        entry.Entity.CreatedById = userId;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}