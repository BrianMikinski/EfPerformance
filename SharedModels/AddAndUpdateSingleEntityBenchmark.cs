
using BenchmarkDotNet.Attributes;

namespace Blog.Benchmarks;

public class AddAndUpdateSingleEntityBenchmark : BenchmarkBase
{
    /// <summary>
    /// Retrieve and update a single entity
    /// </summary>
    [Benchmark]
    public void PostRetrieveAndUpdate()
    {
        var post = _coreBlogContext.Posts.SingleOrDefault();

        post?.UpdateTitle("EF Core will Rock your socks off!");

        _coreBlogContext.SaveChanges();
    }
}