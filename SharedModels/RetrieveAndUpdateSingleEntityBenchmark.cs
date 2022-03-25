using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class RetrieveAndUpdateSingleEntityBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        AddPostsToSeedLimit(1000, true);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts(false);
    }

    [BenchmarkCategory(nameof(RetrieveAndUpdateSingleEntityBenchmark)), Benchmark(Baseline = true)]
    public void PostRetrieveAndUpdateEf6()
    {
        var post = _blogContext.Posts.FirstOrDefault();
        post?.UpdateTitle("Is this faster than EF Core");

        _coreBlogContext.SaveChanges();
    }

    [BenchmarkCategory(nameof(RetrieveAndUpdateSingleEntityBenchmark)), Benchmark]
    public void PostRetrieveAndUpdateEfCore()
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