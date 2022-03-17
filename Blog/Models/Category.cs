using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class Category
{
    public Category()
    {

    }

    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public List<Post> Posts { get; private set; }
}
