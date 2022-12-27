using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

[Config(typeof(StyleConfig))]
public class AddSingleEntityBenchmark : BenchmarkBase
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

        var post = Post.NewPost();

        context.Posts.Add(post);
        context.SaveChanges();
    }

    [Benchmark]
    public void EfCore()
    {
        using var context = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());

        var post = PostCore.NewPost();

        context.Posts.Add(post);
        context.SaveChanges();
    }

    [Benchmark]
    public void EfCorePooled()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        var post = PostCore.NewPost();

        context.Posts.Add(post);
        context.SaveChanges();
    }
}
