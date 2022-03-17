using System.ComponentModel.DataAnnotations;

namespace Blog.Models;
public class Tag
{
    public Tag()
    {
        PostTags = new();
    }

    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public List<PostTag> PostTags { get; private set; }
}
