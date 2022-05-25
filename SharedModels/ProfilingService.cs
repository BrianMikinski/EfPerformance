using Blog.Models;
using CoreBlog.Models;
using System.Diagnostics;

namespace Blog.Benchmarks;

public class ProfilingService : BenchmarkBase
{
    public void SingleEntity()
    {
        using var coreContext = _corePooledDbContextFactory.CreateDbContext();

        // EF Core
        coreContext.Posts.Add(PostCore.NewPost());
        coreContext.SaveChanges();

        Stopwatch coreStopWatch = new();

        coreStopWatch.Start();

        coreContext.Posts.FirstOrDefault();

        coreStopWatch.Stop();

        // EF6

        using var context = new BlogContext();
    
        context.Posts.Add(Post.NewPost());
        context.SaveChanges();

        Stopwatch stopWatch = new();
        stopWatch.Start();

        context.Posts.FirstOrDefault();

        stopWatch.Stop();
    }
}