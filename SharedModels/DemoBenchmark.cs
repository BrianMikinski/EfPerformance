using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class DemoBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
       
    }

    [IterationSetup]
    public void IterationSetup()
    {
        
    }

    /// <summary>
    /// Retrieve single entity
    /// </summary>
    [BenchmarkCategory(nameof(DemoBenchmark)), Benchmark]
    public void NewFunction()
    {
        //throw new NotImplementedException();
        //var firstPost = _coreBlogContext.Posts.FirstOrDefault();
    }

    [BenchmarkCategory(nameof(DemoBenchmark)), Benchmark(Baseline = true)]
    public void OldFunction()
    {
        //throw new NotImplementedException();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        
    }
}