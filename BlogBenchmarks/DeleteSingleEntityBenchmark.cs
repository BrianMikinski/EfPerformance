using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

[Config(typeof(StyleConfig))]
public class DeleteSingleEntityBenchmark :BenchmarkBase
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

        var post = context.Posts.FirstOrDefault();

        if(post != null)
        {
            context.Posts.Remove(post);
            context.SaveChanges();
        }
    }

    [Benchmark]
    public void EfCore()
    {
        using var context = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());

        var post = context.Posts.FirstOrDefault();

        if (post != null)
        {
            context.Posts.Remove(post);
            context.SaveChanges();
        }
    }

    [Benchmark]
    public void EfCorePooled()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        var post = context.Posts.FirstOrDefault();

        if(post != null)
        {
            context.Posts.Remove(post);
            context.SaveChanges();
        }
    }
}
