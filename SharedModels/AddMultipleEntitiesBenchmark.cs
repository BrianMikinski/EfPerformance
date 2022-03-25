using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AddMultipleEntitiesBenchmark : BenchmarkBase
{

    [GlobalSetup]
    public void GlobalSetup()
    {
        SeedLimit = 1000;
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    [BenchmarkCategory(nameof(AddMultipleEntitiesBenchmark)), Benchmark(Baseline = true)]
    public void AddMultipleEntitiesEf6()
    {
        List<Post> posts = new();

        for (int i = 0; i < SeedLimit; i++)
        {
            posts.Add(new Post());
        }

        _blogContext.Posts.AddRange(posts);
        _blogContext.SaveChanges();
    }

    [BenchmarkCategory(nameof(AddMultipleEntitiesBenchmark)), Benchmark]
    public void AddMultipleEntitiesEfCore()
    {
        PostsAddRangeInsertEFCore(SeedLimit);
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}
