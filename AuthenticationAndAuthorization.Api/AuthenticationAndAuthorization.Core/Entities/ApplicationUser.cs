using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
