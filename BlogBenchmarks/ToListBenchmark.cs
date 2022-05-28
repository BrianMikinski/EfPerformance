using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Benchmarks;

public class ToListBenchmark : BenchmarkBase
{
    [Params(1, 10, 1000)]
    public int Rows = 0;

    [GlobalSetup]
    public void GlobalSetup()
    {
        ConfigDatabases();
        AddPostsToSeedLimit(Rows);
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
    public void EfCore()
    {
        using var context = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());
        _ = context
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