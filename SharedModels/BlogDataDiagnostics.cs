namespace Blog.Benchmarks;

public class BlogDataDiagnostics
{
    public int? PostsCount { get; init; }

    public int? PostTagsCount { get; init; }

    public int? CategoriesCount { get; init; }

    public int? TagsCount { get; init; }

    public void PrintTableDiagnostics()
    {
        Console.WriteLine($"Current Table Row Counts:");
        Console.WriteLine($"Total Posts: {PostsCount}");
        Console.WriteLine($"Total PostTags: {PostTagsCount}");
        Console.WriteLine($"Total Categories: {CategoriesCount}");
        Console.WriteLine($"Total Tags: {TagsCount}");
    }
}
