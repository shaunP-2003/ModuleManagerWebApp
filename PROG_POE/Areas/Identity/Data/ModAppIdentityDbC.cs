using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG_POE.Models.Domain;

namespace PROG_POE.Areas.Identity.Data;

public class ModAppIdentityDbC : IdentityDbContext<AppUser>
{
    public ModAppIdentityDbC(DbContextOptions<ModAppIdentityDbC> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        
    }
}
