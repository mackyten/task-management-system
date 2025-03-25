using Microsoft.AspNetCore.Identity;

namespace TMS.DOMAIN.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public string? Suffix { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}