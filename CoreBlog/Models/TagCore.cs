using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBlog.Models;

[Table("Tags")]
public class TagCore
{
    public static TagCore NewTag() => new()
    {
        Id = Guid.NewGuid(),
        Name = "EF Core"
    };

    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public IEnumerable<PostTagCore> PostTags { get; set; }
}