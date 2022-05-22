using BenchmarkDotNet.Attributes;
using System.Data.Entity;

namespace Blog.Benchmarks;

[MemoryDiagnoser]
[RPlotExporter]
public class RetrieveAllPostsBenchmark : BenchmarkBase
{
    [Params(1, 10, 1000)]
    public int Rows;

    [GlobalSetup]
    public void GlobalSetup()
    {
        ConfigDatabases();
        AddPostsToSeedLimit(Rows, true);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        NewDbContexts();
    }

    [Benchmark(Baseline = true)]
    public void Ef6()
    {
        _ = _blogContext
                .Posts
                .AsNoTracking()
                .ToList();
    }

    [Benchmark]
    public void Ef6Singleton()
    {
        _ = _blogContextSingleton
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [Benchmark]
    public void EfCore()
    {
        _ = _coreBlogContext
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [Benchmark]
    public void EfCoreSingleton()
    {
        _ = _coreBlogContextSingleton
                    .Posts
                    .AsNoTracking()
                    .ToList();
    }

    [Benchmark]
    public void EfCorePooled()
    {
        _ = _coreBlogContextPooled.Posts
                .AsNoTracking()
                .ToList();
    }
}