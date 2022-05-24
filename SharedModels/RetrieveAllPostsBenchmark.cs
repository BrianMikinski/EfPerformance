using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;
using System.Data.Entity;

namespace Blog.Benchmarks;

public class RetrieveAllPostsBenchmark : BenchmarkBase
{
    [Params(1, 10, 1000)]
    public int Rows;

    [GlobalSetup]
    public void GlobalSetup()
    {
        ConfigDatabases();
        AddPostsToSeedLimit(Rows, true);
    }

    [Benchmark(Baseline = true)]
    public void Ef6()
    {
        using var context = new BlogContext();
        _ = context
                .Posts
                .AsNoTracking()
                .ToList();
    }

    [Benchmark]
    public void Ef6Singleton()
    {
        _ = _blogContextSingleton
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [Benchmark]
    public void EfCore()
    {
        using var context = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());
        _ = context
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [Benchmark]
    public void EfCoreSingleton()
    {
        _ = _coreBlogContextSingleton
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [Benchmark]
    public void EfCorePooled()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        _ = context.Posts
                .AsNoTracking()
                .ToList();
    }
}