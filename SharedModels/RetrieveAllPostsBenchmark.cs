using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Data.Entity;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[RPlotExporter]
public class RetrieveAllPostsBenchmark : BenchmarkBase
{
    [Params(1000)]
    public int Rows;

    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit(Rows, true);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    [BenchmarkCategory(nameof(Ef6NewContextRetrieveAllPosts)), ]
    [Benchmark(Baseline = true)]
    public void Ef6NewContextRetrieveAllPosts()
    {


        _ = _blogContext
                .Posts
                .AsNoTracking()
                .ToList();
    }

    [BenchmarkCategory(nameof(Ef6SingletonRetrieveAllPosts))]
    [Benchmark]
    public void Ef6SingletonRetrieveAllPosts()
    {
        _ = _blogContextSingleton
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [BenchmarkCategory(nameof(EfCoreNewContextRetrieveAllPosts))]
    [Benchmark]
    public void EfCoreNewContextRetrieveAllPosts()
    {
        _ = _coreBlogContext
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [BenchmarkCategory(nameof(EfCoreSingletonRetrieveAllPosts))]
    [Benchmark]
    public void EfCoreSingletonRetrieveAllPosts()
    {
        _ = _coreBlogContextSingleton
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [BenchmarkCategory(nameof(EfCorePooledRetrieveAllPosts))]
    [Benchmark]
    public void EfCorePooledRetrieveAllPosts()
    {
        _ = _coreBlogContextPooled.Posts
                .AsNoTracking()
                .ToList();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}