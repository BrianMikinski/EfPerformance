using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class RetrieveAndUpdateSinglePostBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit(1000, true);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    [BenchmarkCategory(nameof(Ef6RetrieveAndUpdatePost)), Benchmark(Baseline = true)]
    public void Ef6RetrieveAndUpdatePost()
    {
        var post = _blogContext.Posts.FirstOrDefault();
        post?.UpdateTitle("Is this faster than EF Core");

        _coreBlogContext.SaveChanges();
    }

    [BenchmarkCategory(nameof(EfCoreRetrieveAndUpdatePost)), Benchmark]
    public void EfCoreRetrieveAndUpdatePost()
    {
        var post = _coreBlogContext.Posts.FirstOrDefault();
        post?.UpdateTitle("EF Core will Rock your socks off!");

        _coreBlogContext.SaveChanges();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}