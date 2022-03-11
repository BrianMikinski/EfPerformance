using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name {get; set;}
}