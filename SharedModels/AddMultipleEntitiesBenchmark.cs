using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

public class AddMultipleEntitiesBenchmark : BenchmarkBase
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

        List<Post> posts = new();

        for (int i = 0; i < SeedLimit; i++)
        {
            context.Posts.Add(new Post());
        }

        context.SaveChanges();
    }

    [Benchmark]
    public void EfCorePooled()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        List<PostCore> posts = new();

        for (int i = 0; i < SeedLimit; i++)
        {
            context.Posts.Add(new PostCore());
        }

        context.SaveChanges();
    }
}
