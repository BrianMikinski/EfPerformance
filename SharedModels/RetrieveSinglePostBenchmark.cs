using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

public class RetrieveSinglePostBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        ConfigDatabases();
        AddPostsToSeedLimit();
    }

    [Benchmark(Baseline = true)]
    public void Ef6()
    {
        using var context = new BlogContext();
        _ = context.Posts.FirstOrDefault();
    }

    [Benchmark]
    public void Ef6Singleton()
    {
        _ = _blogContextSingleton.Posts.FirstOrDefault();
    }


    [Benchmark]
    public void EfCore()
    {
        using var context = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());
        _ = context.Posts.FirstOrDefault();
    }

    [Benchmark]
    public void EfCoreSingleton()
    {
        _coreBlogContextSingleton.Posts.FirstOrDefault();
    }

    [Benchmark]
    public void EfCorePooled()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();
        _ = context.Posts.FirstOrDefault();
    }
}