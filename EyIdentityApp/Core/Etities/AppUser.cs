using Microsoft.AspNetCore.Identity;

namespace Core.Etities
{
    public class AppUser : IdentityRole<int>
    {
        public string ImagePath { get; set; }
    }
}
