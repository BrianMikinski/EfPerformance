﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBlog.Models;

[Table("PostTags")]
public  class PostTagCore
{
    [Key]
    public Guid Id { get; set; }

    public Guid PostId { get; init; }
    
    public PostCore Post { get; init; }

    public Guid TagId { get; init; }
    public TagCore Tag { get; init; }
}
