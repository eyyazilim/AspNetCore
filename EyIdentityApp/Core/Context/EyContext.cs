using Core.Etities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class EyContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EyContext(DbContextOptions<EyContext> options) : base(options)
        {
            
        }
    }
}
