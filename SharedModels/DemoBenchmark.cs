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

    [BenchmarkCategory(nameof(DemoBenchmark)), Benchmark]
    public void NewFunction()
    {

    }

    [BenchmarkCategory(nameof(DemoBenchmark)), Benchmark(Baseline = true)]
    public void OldFunction()
    {

    }

    [IterationCleanup]
    public void IterationCleanup()
    {

    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        
    }
}