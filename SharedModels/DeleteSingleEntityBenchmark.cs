using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class DeleteSingleEntityBenchmark :BenchmarkBase
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

    [BenchmarkCategory(nameof(DeleteSingleEntityBenchmark)), Benchmark(Baseline = true)]
    public void DeleteSingleEntityEf()
    {
        var post = _blogContext.Posts.FirstOrDefault();

        if(post != null)
        {
            _blogContext.Posts.Remove(post);
            _blogContext.SaveChanges();
        }
    }

    [BenchmarkCategory(nameof(DeleteSingleEntityBenchmark)), Benchmark]
    public void DeleteSingleEntityEfCore()
    {
        var post = _coreBlogContext.Posts.FirstOrDefault();

        if(post != null)
        {
            _coreBlogContext.Posts.Remove(post);
            _coreBlogContext.SaveChanges();
        }
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        //BaseCleanup();
    }
}
