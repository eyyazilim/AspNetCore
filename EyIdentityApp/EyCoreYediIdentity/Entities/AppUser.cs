using Microsoft.AspNetCore.Identity;

namespace EyCoreYediIdentity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string ImagePath { get; set; }
    }
}
