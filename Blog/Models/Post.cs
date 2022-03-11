using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Post
    {
        public Post()
        {
            Category = new();
            Tags = new List<Tag>();
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? Title { get; set; }

        [StringLength(1000)]
        public string? Content { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}