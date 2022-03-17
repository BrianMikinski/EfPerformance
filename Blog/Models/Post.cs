using System.ComponentModel.DataAnnotations;

namespace Blog.Models;
public class Post
{
    public static Post NewPost()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Title = "EF6 is old school!",
            Content = "Some cool old school EF6 stuff"
        };
    }

    public Post()
    {
        Category = new();
        Tags = new List<Tag>();
    }

    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [StringLength(1000)]
    public string? Content { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }

    public IEnumerable<Tag> Tags { get; set; }
}