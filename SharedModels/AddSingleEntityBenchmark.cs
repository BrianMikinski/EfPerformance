using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AddSingleEntityBenchmark : BenchmarkBase
{
    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    [BenchmarkCategory(nameof(AddSingleEntityBenchmark)), Benchmark(Baseline = true)]
    public void AddSingleEntityEf6()
    {
        var post = Post.NewPost();

        _blogContext.Posts.Add(post);
        _blogContext.SaveChanges();
    }

    [BenchmarkCategory(nameof(AddSingleEntityBenchmark)), Benchmark]
    public void AddSingleEntityEfCore()
    {
        var post = PostCore.NewPost();
        _coreBlogContext.Posts.Add(post);
        _coreBlogContext.SaveChanges();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}
