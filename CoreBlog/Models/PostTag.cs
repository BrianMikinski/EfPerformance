namespace CoreBlog.Models;

public  class PostTag
{
    public Guid PostId { get; init; }
    public Post Post { get; init; }

    public Guid TagId { get; init; }
    public Tag Tag { get; init; }
}
