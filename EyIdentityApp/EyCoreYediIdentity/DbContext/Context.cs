using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EyCoreYediIdentity.Entities;

namespace EyCoreYediIdentity.DbContext
{
    public class Context :IdentityDbContext<AppUser, AppRole, int>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
    }
}
