using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class RetrieveManyEntitiesBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit(10000, true);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    /// <summary>
    /// Retrieve and update a single entity with ef 6
    /// </summary>
    [BenchmarkCategory(nameof(RetrieveManyEntitiesBenchmark)), Benchmark(Baseline = true)]
    public void PostRetrieveListEf6()
    {
        var post = _blogContext.Posts.ToList();
    }

    /// <summary>
    /// Retrieve and update a single entity with ef core
    /// </summary>
    [BenchmarkCategory(nameof(RetrieveManyEntitiesBenchmark)), Benchmark]
    public void PostRetrieveListEfCore()
    {
        var posts = _coreBlogContext.Posts.ToList();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}