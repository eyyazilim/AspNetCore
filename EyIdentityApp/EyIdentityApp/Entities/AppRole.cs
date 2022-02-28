using Microsoft.AspNetCore.Identity;
using System;

namespace EyIdentityApp.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}
