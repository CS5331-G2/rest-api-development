﻿using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using diary.Models;

namespace diary.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Diary> Diaries { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Diary>(b => 
                {
                    b.HasKey(d => d.Id);
                    b.HasOne<ApplicationUser>()
                        .WithMany(a => a.Diaries)
                        .OnDelete(DeleteBehavior.Cascade);
                    b.ToTable("Diary");
                });

            builder.Entity<ApplicationUser>(b =>
                {
                    b.Property(a => a.UuidV4Token)
                        .HasMaxLength(36);
                });
        }

        public ApplicationUser GetUserWithToken(string uuidV4Token)
        {
            return Users.Where(p => p.UuidV4Token == uuidV4Token).FirstOrDefault();
        }
    }
}
