using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Blog.Benchmarks;

//Console.WriteLine("Hello, World! Welcome to EF Core vs Ef6 Performance profiling!");
BenchmarkSwitcher.FromAssembly(typeof(BenchmarkBase).Assembly).Run(args, GetGlobalConfig());

DebugBenchmarks();

DbDiagnostics();


/// <summary>
/// Programatic configuration of the jobs.
/// This can be overwridden by passing in arguments
/// </summary>
static IConfig GetGlobalConfig()
    => DefaultConfig.Instance.AddJob(Job.Default
            .WithWarmupCount(1)
            .AsDefault());

/// <summary>
/// Method for debugging individual benchmark functionality
/// </summary>
static void DebugBenchmarks(bool isEnabled = false)
{
    if (isEnabled)
    {
        var allPostsBenchmark = new RetrieveAllPostsBenchmark();

        allPostsBenchmark.GlobalSetup();

        allPostsBenchmark.EfCorePooled();

        allPostsBenchmark.Ef6();
    }
}

/// <summary>
/// Print database diagnostics to the console
/// </summary>
static void DbDiagnostics(bool isEnabled = false)
{
    if (isEnabled)
    {
        Console.WriteLine("Post profiling statistics: ");
        var ef6ProfilingService = new ProfilingService();

        var (coreBlogDiagnostics, blogDiagnostics) = ef6ProfilingService.TableDiagnostics();

        coreBlogDiagnostics.PrintTableDiagnostics();
        blogDiagnostics.PrintTableDiagnostics(false);
    }
}