using BenchmarkDotNet.Attributes;
using CoreBlog.Models;

namespace Blog.Benchmarks;

[MinColumn]
[MaxColumn]
public class EfCoreProfilingService : BenchmarkBase
{


   

    /// <summary>
    /// Retrieve single entity
    /// </summary>
    //[Benchmark]
    public void PostRetrievalCore()
    {
        var firstPost = _coreBlogContext.Posts.SingleOrDefault();
    }

    public void PostDelete()
    {

    }

    public void PostInsertNoChangeTracker()
    {
        //bool changeTracking = false
    }

    [IterationCleanup]
    public void IterationCleanup()
    {

    }
}