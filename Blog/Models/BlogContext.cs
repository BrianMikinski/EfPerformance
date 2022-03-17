using System.Data.Entity;

namespace Blog.Models;

public class BlogContext : DbContext
{
    public BlogContext()
        : base("Server=(localdb)\\mssqllocaldb;Database=CoreBlog;Trusted_Connection=True;")
    {
        Configuration.LazyLoadingEnabled = false; 
    }

    public DbSet<Post> Posts { get; set; }

    public DbSet<PostTag> PostTags { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
       
    }
}
