using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Models;

public class Post
{
    public Post()
    {
        Category = new ();
        Tags = new List<Tag>();
    }

    public static Post NewPost(Category category, IEnumerable<Tag> tags) => new()
    { 
        Id = Guid.NewGuid(),
        Category = category,
        Tags = tags,
        Title = "EF Core is awesome!",
        Content = "Some ramblings on EF Core",
    };

    [Key]
    public Guid Id {get; init;}

    [StringLength(50)]
    public string? Title {get; init;}

    [StringLength(1000)]
    public string? Content {get; init;}
    
    public Guid CategoryId {get; init;}

    public Category Category {get; init;}

    public IEnumerable<Tag> Tags {get; init;}
}
