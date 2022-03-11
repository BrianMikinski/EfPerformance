using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Models;

public class Tag
{
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name {get; set;}
}