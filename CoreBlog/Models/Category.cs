using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Models;

public class Category
{
    public static Category NewCategory() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Software Engineering"
    };

    [Key]
    public Guid Id { get; init; }

    [StringLength(50)]
    public string Name { get; init; }
}