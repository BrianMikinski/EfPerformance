using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Models;

public class Tag
{
    public static Tag NewTag() => new()
    {
        Id = Guid.NewGuid(),
        Name = "EF Core"
    };

    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }
}