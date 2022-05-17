using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class RetrieveSinglePostBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit();
    }

    [BenchmarkCategory(nameof(Ef6NewContextPostRetrieveSingleEntity))]
    [Benchmark(Baseline = true)]
    public void Ef6NewContextPostRetrieveSingleEntity()
    {
        using var context = new BlogContext();
        _ = context.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(Ef6SingletonPostRetrieveSingleEntity))]
    [Benchmark]
    public void Ef6SingletonPostRetrieveSingleEntity()
    {
        _ = _blogContextSingleton.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(EfCoreNewContextPostRetrieveSingleEntity))]
    [Benchmark]
    public void EfCoreNewContextPostRetrieveSingleEntity()
    {
        using var context = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());
        _ = context.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(EFCoreSingletonPostRetrieveSingleEntity))]
    [Benchmark]
    public void EFCoreSingletonPostRetrieveSingleEntity()
    {
        _coreBlogContextSingleton.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(EfCorePooledPostRetrieveSingleEntity))]
    [Benchmark]
    public void EfCorePooledPostRetrieveSingleEntity()
    {
        using var context = _coreDbContextFactory.CreateDbContext();
        _ = context.Posts.FirstOrDefault();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}