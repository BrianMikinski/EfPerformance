using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBlog.Models;

[Table("Posts")]
public class PostCore
{
    public PostCore()
    {
        
    }

    public static (PostCore, IEnumerable<PostTagCore>) NewPostWithCategoriesAndTags(CategoryCore category, IEnumerable<TagCore> tags)
    {
        var post = new PostCore()
        {
            Id = Guid.NewGuid(),
            Category = category,
            Title = "EF Core is awesome!",
            Content = "Some ramblings on EF Core",
        };

        var postTags = new List<PostTagCore>();

        foreach(var tag in tags)
        {
            postTags.Add(new PostTagCore()
            {
                TagId = tag.Id,
                PostId = post.Id
            });
        }

        return (post, postTags);
    }

    public static PostCore NewPost()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Title = "EF Core is awesome!",
            Content = "Some cool EF Core stuff"
        };
    }

    public void ChangeId()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id {get; private set;}

    [StringLength(50)]
    public string? Title {get; private set;}

    public void UpdateTitle(string newTitle)
    {
        Title = newTitle;
    }

    [StringLength(1000)]
    public string? Content {get; init;}
    
    public Guid? CategoryId {get; init;}

    public CategoryCore? Category {get; init;}

    public IEnumerable<PostTagCore>? PostTags { get; set; }
}
