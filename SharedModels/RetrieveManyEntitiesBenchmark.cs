using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Data.Entity;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class RetrieveManyEntitiesBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit(100000, true);
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
        var posts = _blogContext
                     .Posts
                     .AsNoTracking()
                     .ToList();
    }

    /// <summary>
    /// Retrieve and update a single entity with ef core
    /// </summary>
    [BenchmarkCategory(nameof(RetrieveManyEntitiesBenchmark)), Benchmark]
    public void PostRetrieveListEfCore()
    {
        var posts = _coreBlogContext
                      .Posts
                      .AsNoTracking()
                      .ToList();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}