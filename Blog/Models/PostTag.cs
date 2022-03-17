using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models;

public class PostTag
{
    public Guid PostId { get; init; }
    public Post Post { get; init; }

    public Guid TagId { get; init; }
    public Tag Tag { get; init; }
}
