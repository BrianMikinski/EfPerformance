using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Blog.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class BulkInsertEntitiesBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        SeedLimit = 10000;
    }


    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    [BenchmarkCategory(nameof(BulkInsertEntitiesBenchmark)), Benchmark(Baseline = true)]
    public void AddRangeEntitiesEfCore()
    {
        //RangeInsertEFCore();
    }

    [BenchmarkCategory(nameof(BulkInsertEntitiesBenchmark)), Benchmark]
    public void BulkInsertEntitiesEfCore()
    {
        PostsAddBulkInsertEfCore(SeedLimit);
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        BaseCleanup();
    }
}
