using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Text;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
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

    [BenchmarkCategory(nameof(DemoBenchmark))]
    [Benchmark(Baseline = true)]
    public void OldFunction()
    {
        string firstName = "John";
        string lastName = "Doe";
        string name = firstName + " " + lastName;
    }

    [BenchmarkCategory(nameof(DemoBenchmark))]
    [Benchmark]
    public void NewFunction()
    {
        string firstName = "John";
        string lastName = "Doe";
        StringBuilder sb = new(firstName);

        string name = sb.Append(" ")
            .Append(lastName)
            .ToString();
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