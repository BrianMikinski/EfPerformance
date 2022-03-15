using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBlog.Models;

[Table("Categories")]
public class CategoryCore
{
    public static CategoryCore NewCategory() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Software Engineering"
    };

    [Key]
    public Guid Id { get; init; }

    [StringLength(50)]
    public string Name { get; init; }
}