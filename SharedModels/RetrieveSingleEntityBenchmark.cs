using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class RetrieveSingleEntityBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit();
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    /// <summary>
    /// Retrieve single entity
    /// </summary>
    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark)), Benchmark]
    public void PostRetrieveSingleEntityEfCore()
    {
        _coreBlogContext.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark)), Benchmark(Baseline = true)]
    public void PostRetrieveSingleEntityEf()
    {
        _blogContext.Posts.FirstOrDefault();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}