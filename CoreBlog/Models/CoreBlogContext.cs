using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreBlog.Models
{
    public partial class CoreBlogContext : DbContext
    {
        public DbSet<Post> Posts {get; set;}

        public DbSet<Category> Categories {get; set;}

        public DbSet<Tag> Tags {get; set;}

        public CoreBlogContext()
        {
        }

        public CoreBlogContext(DbContextOptions<CoreBlogContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CoreBlog;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
