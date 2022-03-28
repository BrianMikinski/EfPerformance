using Blog.Models;
using CoreBlog.Models;
using System.Diagnostics;

namespace Blog.Benchmarks;

public class ProfilingService : BenchmarkBase
{
    public void TestSingleEntity()
    {
        // EF Core
        _coreBlogContext.Posts.Add(PostCore.NewPost());

        _coreBlogContext.SaveChanges();

        _coreBlogContext = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());

        Stopwatch coreStopWatch = new();

        coreStopWatch.Start();

        _coreBlogContext.Posts.FirstOrDefault();

        coreStopWatch.Stop();

        // EF6
        _blogContext.Posts.Add(Post.NewPost());
        _blogContext.SaveChanges();

        _blogContext = new BlogContext();

        Stopwatch stopWatch = new();
        stopWatch.Start();

        _blogContext.Posts.FirstOrDefault();

        stopWatch.Stop();
    }
}