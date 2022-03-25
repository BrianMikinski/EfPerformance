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

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark)), Benchmark(Baseline = true)]
    public void PostRetrieveSingleEntityEf()
    {
        _blogContext.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark)), Benchmark]
    public void PostRetrieveSingleEntityEfCore()
    {
        _coreBlogContext.Posts.FirstOrDefault();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}