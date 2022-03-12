using BenchmarkDotNet.Attributes;
using CoreBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreBlog;

[MinColumn]
[MaxColumn]
public class EfCoreProfilingService
{
    private CoreBlogContext _coreBlogContext;

    public EfCoreProfilingService()
    {
        _coreBlogContext = new CoreBlogContext();
    }

    public void SeedData()
    {
        
    }

    public void CountData()
    {

    }

    [GlobalSetup]
    public void GlobalSetup()
    {

    }

    [IterationSetup]
    public void IterationSetup()
    {
        _coreBlogContext = new CoreBlogContext();
    }

    [Benchmark]
    public void SingleRecord()
    {

    }

    public void DatabaseSeed()
    {

    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {

    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        _coreBlogContext.Tags.FromSqlRaw("TRUNCATE TABLE [Posts]");
        _coreBlogContext.Tags.FromSqlRaw("TRUNCATE TABLE [Categories]");
        _coreBlogContext.Tags.FromSqlRaw("TRUNCATE TABLE [Tags]");
    }
}