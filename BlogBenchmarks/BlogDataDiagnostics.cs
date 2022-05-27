namespace Blog.Benchmarks;

public class BlogDataDiagnostics
{
    public int? PostsCount { get; init; }

    public int? PostTagsCount { get; init; }

    public int? CategoriesCount { get; init; }

    public int? TagsCount { get; init; }

    public void PrintTableDiagnostics(bool isCoreTable = true)
    {
        var table = isCoreTable ? "Core Blog" : "Blog";

        Console.WriteLine($"{table} Table Row Counts:");
        Console.WriteLine();
        Console.WriteLine($"Total Posts: {PostsCount}");
        Console.WriteLine($"Total PostTags: {PostTagsCount}");
        Console.WriteLine($"Total Categories: {CategoriesCount}");
        Console.WriteLine($"Total Tags: {TagsCount}");
        Console.WriteLine();
    }
}
