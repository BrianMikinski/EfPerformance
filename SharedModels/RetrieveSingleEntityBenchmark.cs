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
    public void PostRetrieveSingleEntityEf6NewContext()
    {
        _blogContext.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark]
    public void PostRetrieveSingleEntityEf6Singleton()
    {
        _blogContextSingleton.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark]
    public void PostRetrieveSingleEntityEfCoreNewContext()
    {
        _coreBlogContext.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark]
    public void PostRetrieveSingleEntityEfCoreSingleton()
    {
        _coreBlogContextSingleton.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(RetrieveSingleEntityBenchmark))]
    [Benchmark]
    public void PostRetrieveSingleEntityEfCoreFactory()
    {
        _coreBlogContextFromFactory.Posts.FirstOrDefault();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}