using BenchmarkDotNet.Attributes;

namespace Blog.Benchmarks;

[MemoryDiagnoser]
public class DemoBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
       // code that is run once prior to all BenchmarkDotNet iterations
    }

    [IterationSetup]
    public void IterationSetup()
    {
        // code that is run once prior to each iteration
    }

    [Benchmark(Baseline = true)]
    public void OldFunction()
    {
        Thread.Sleep(50);
    }

    [Benchmark]
    public void NewFunction()
    {
        Thread.Sleep(100);
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        // code run once after each iteration
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
       // code run once after all iterations have run
    }
}