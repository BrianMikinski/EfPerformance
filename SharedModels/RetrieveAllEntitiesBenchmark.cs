using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Data.Entity;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[RPlotExporter]
public class RetrieveAllEntitiesBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit(1000, true);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    [BenchmarkCategory(nameof(RetrieveAllEntitiesBenchmark)), Benchmark(Baseline = true)]
    public void PostRetrieveListEf6()
    {
        var posts = _blogContext
                     .Posts
                     .AsNoTracking()
                     .ToList();
    }

    [BenchmarkCategory(nameof(RetrieveAllEntitiesBenchmark)), Benchmark]
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