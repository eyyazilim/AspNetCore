using Microsoft.AspNetCore.Identity;

namespace EyCoreYediIdentity.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}
