using Microsoft.AspNetCore.Identity;

namespace Core.Etities
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}
