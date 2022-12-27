using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

[Config(typeof(StyleConfig))]
public class AddMultipleEntitiesBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        ConfigDatabases();
    }

    [Benchmark(Baseline = true)]
    public void Ef6()
    {
        using var context = new BlogContext();

        for (int i = 0; i < SeedLimit; i++)
        {
            context.Posts.Add(new Post());
        }

        context.SaveChanges();
    }

    [Benchmark]
    public void EfCore()
    {
        using var context = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());

        for (int i = 0; i < SeedLimit; i++)
        {
            context.Posts.Add(new PostCore());
        }

        context.SaveChanges();
    }

    [Benchmark]
    public void EfCorePooled()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        for (int i = 0; i < SeedLimit; i++)
        {
            context.Posts.Add(new PostCore());
        }

        context.SaveChanges();
    }
}
