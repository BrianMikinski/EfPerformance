using BenchmarkDotNet.Attributes;

namespace Blog.Benchmarks;

public class RetrieveSingleEntityBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit();
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    /// <summary>
    /// Retrieve single entity
    /// </summary>
    [Benchmark]
    public void PostRetrievalCore()
    {
        var firstPost = _coreBlogContext.Posts.SingleOrDefault();
    }

    [Benchmark]
    public void PostRetrieval()
    {
        var firstPost = _blogContext.Posts.SingleOrDefault();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}