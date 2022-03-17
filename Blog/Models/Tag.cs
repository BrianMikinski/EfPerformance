using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

public class Tag
{
    public Tag()
    {
        PostTags = new();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public List<PostTag> PostTags { get; private set; }
}
