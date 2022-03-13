using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Models;

public class Post
{
    public Post()
    {
        Category = new ();
    }

    public static (Post, IEnumerable<PostTag>) NewPost(Category category, IEnumerable<Tag> tags)
    {
        var post = new Post()
        {
            Id = Guid.NewGuid(),
            Category = category,
            Title = "EF Core is awesome!",
            Content = "Some ramblings on EF Core",
        };

        var postTags = new List<PostTag>();

        foreach(var tag in tags)
        {
            postTags.Add(new PostTag()
            {
                TagId = tag.Id,
                PostId = post.Id
            });
        }

        return (post, postTags);
    }

    [Key]
    public Guid Id {get; init;}

    [StringLength(50)]
    public string? Title {get; init;}

    [StringLength(1000)]
    public string? Content {get; init;}
    
    public Guid CategoryId {get; init;}

    public Category Category {get; init;}

    public IEnumerable<PostTag> PostTags { get; set; }
}
