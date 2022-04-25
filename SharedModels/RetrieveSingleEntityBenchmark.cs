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

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark(Baseline = true)]
    public void Ef6NewContextPostRetrieveSingleEntity()
    {
        _blogContext.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark]
    public void Ef6SingletonPostRetrieveSingleEntity()
    {
        _blogContextSingleton.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark]
    public void EfCoreNewContextPostRetrieveSingleEntity()
    {
        _coreBlogContext.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark]
    public void EFCoreSingletonPostRetrieveSingleEntity()
    {
        _coreBlogContextSingleton.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark]
    public void EfCorePooledPostRetrieveSingleEntity()
    {
        _coreBlogContextPooled.Posts.FirstOrDefault();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}