using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Blog.Benchmarks;
using CoreBlog;

Console.WriteLine("Hello, World! Welcome to the EF Performance Contest");

Console.WriteLine("EF Core 6 Profiling Service");
var benchmarkBase = new BenchmarkBase();

//coreProfilingService.DatabaseSeed();
BenchmarkRunner.Run<RetrieveSingleEntityBenchmark>();

//Console.WriteLine("EF6 Profiling Service");
//var ef6ProfilingService = new Ef6ProfilingService();

var (coreBlogDiagnostics,  blogDiagnostics)= benchmarkBase.TableDiagnostics();

coreBlogDiagnostics.PrintTableDiagnostics();

blogDiagnostics.PrintTableDiagnostics(false);