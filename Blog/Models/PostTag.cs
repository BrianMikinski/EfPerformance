using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

public class PostTag
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid PostId { get; init; }

    public Post Post { get; init; }

    public Guid TagId { get; init; }
    public Tag Tag { get; init; }
}
