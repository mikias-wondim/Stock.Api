using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stock.Api.Models;

namespace Stock.Api.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                
                modelBuilder.Entity<Comment>()
                    .HasOne(c => c.Stock)
                    .WithMany(s => s.Comments)
                    .HasForeignKey(c => c.StockId)
                    .OnDelete(DeleteBehavior.Cascade); // Configure cascading delete

                base.OnModelCreating(modelBuilder);

                // Creating identity role with predefined default roles
                List<IdentityRole> roles = new List<IdentityRole> {
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },

                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    },
                };

                // seeding the list of roles into the starting model
                modelBuilder.Entity<IdentityRole>().HasData(roles);

            }
    }
    
}