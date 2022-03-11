using System.Data.Entity;

namespace Blog.Models;

public class SchoolContext : DbContext
{
    public SchoolContext()
        : base("Server=(localdb)\\mssqllocaldb;Database=CoreBlog;Trusted_Connection=True;")
    {
        
    }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
       
    }
}
