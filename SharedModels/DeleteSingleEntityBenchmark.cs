using BenchmarkDotNet.Attributes;

namespace Blog.Benchmarks;

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
       throw new NotImplementedException();
    }

    [BenchmarkCategory(nameof(DeleteSingleEntityBenchmark)), Benchmark]
    public void DeleteSingleEntityEfCore()
    {
        throw new NotImplementedException();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}
