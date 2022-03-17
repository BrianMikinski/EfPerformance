using BenchmarkDotNet.Attributes;

namespace Blog.Benchmarks;

public class AddAndUpdateSingleEntityBenchmark : BenchmarkBase
{
    /// <summary>
    /// Retrieve and update a single entity with ef core
    /// </summary>
    [Benchmark]
    public void PostRetrieveAndUpdateEfCore()
    {
        var post = _coreBlogContext.Posts.SingleOrDefault();

        post?.UpdateTitle("EF Core will Rock your socks off!");

        _coreBlogContext.SaveChanges();
    }

    /// <summary>
    /// Retrieve and update a single entity with ef 6
    /// </summary>
    [Benchmark]
    public void PostRetrieveAndUpdateEf6()
    {

    }
}