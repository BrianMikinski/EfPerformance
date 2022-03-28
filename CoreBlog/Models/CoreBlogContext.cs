using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreBlog.Models
{
    public partial class CoreBlogContext : DbContext
    {
        public DbSet<PostCore> Posts { get; set; }

        public DbSet<PostTagCore> PostTags { get; set; }

        public DbSet<CategoryCore> Categories { get; set; }

        public DbSet<TagCore> Tags { get; set; }

        public static DbContextOptions<CoreBlogContext> NewDbContextOptions()
        {
            return new DbContextOptionsBuilder<CoreBlogContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CoreBlog;Trusted_Connection=True;")
            .Options;
        }

        public CoreBlogContext(DbContextOptions<CoreBlogContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<PostTagCore>()
                .HasKey(t => new { t.PostId, t.TagId });

            modelBuilder.Entity<PostTagCore>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTagCore>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
