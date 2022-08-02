using BenchmarkDotNet.Attributes;
using CoreBlog.Models;

namespace Blog.Benchmarks;

public class BulkInsertEntitiesBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        ConfigDatabases();
    }

    [Benchmark(Baseline = true)]
    public void EfCoreAddRange()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        List<PostCore> posts = new();

        for (int i = 0; i < SeedLimit; i++)
        {
            posts.Add(new PostCore());
        }

        context.Posts.AddRange(posts);
        context.SaveChanges();
    }

    [Benchmark]
    public void EfCoreBulkInsertZzzEfExtensions()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        List<PostCore> posts = new();

        for (int i = 0; i < SeedLimit; i++)
        {
            posts.Add(PostCore.NewPost());
        }

        context.BulkInsert(posts);
    }
}
