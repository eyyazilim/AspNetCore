using EyIdentityApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EyIdentityApp.Context
{
    public class EyContext :IdentityDbContext<AppUser,AppRole,int>
    {
        public EyContext(DbContextOptions<EyContext> options):base(options)
        {
             
        }
    }
}
