using BenchmarkDotNet.Attributes;
using Blog.Models;

namespace Blog.Benchmarks;

public class RetrieveAndUpdateSinglePostBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        ConfigDatabases();
        AddPostsToSeedLimit(10000);
    }

    [Benchmark(Baseline = true)]
    public void Ef6()
    {
        using var context = new BlogContext();
        
        var post = context.Posts.FirstOrDefault();
        post?.UpdateTitle("Is this faster than EF Core");

        context.SaveChanges();
    }

    [Benchmark]
    public void EfCorePooled()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();
        
        var post = context.Posts.FirstOrDefault();
        post?.UpdateTitle("EF Core will Rock your socks off!");

        context.SaveChanges();
    }
}