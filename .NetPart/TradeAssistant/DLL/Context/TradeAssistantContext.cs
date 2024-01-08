using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;

namespace DLL.Context
{
    public class TradeAssistantContext:IdentityDbContext<ApplicationUser>
    {
        public TradeAssistantContext(DbContextOptions<TradeAssistantContext> options):base (options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole { 
                Id="1",
                Name="SuperAdmin",
                NormalizedName="SUPERADMIN",
                
                
            });
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "1",
                UserName="SuperAdmin",
                FirstName="SuperAdmin",
                LastName="SuperAdmin",
                NormalizedUserName ="SUPERADMIN",
                SecurityStamp=Guid.NewGuid().ToString()
            });
            base.OnModelCreating(builder);
        }

    }
}
