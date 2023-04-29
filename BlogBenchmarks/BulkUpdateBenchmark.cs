using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

[Config(typeof(StyleConfig))]
public class BulkUpdateBenchmark : BenchmarkBase
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        ConfigDatabases();
        AddPostsToSeedLimit();
    }

    [Benchmark]
    public void Ef6BulkInsertZzzEfExtensions()
    {
        throw new NotImplementedException();

        using var context = new BlogContext();

        List<Post> posts = new();

        for (int i = 0; i < SeedLimit; i++)
        {
            posts.Add(new Post());
        }

        context.BulkUpdate(posts);
    }

    [Benchmark]
    public void EfCoreExecuteUpdate()
    {
        throw new NotImplementedException();

        using var context = _corePooledDbContextFactory.CreateDbContext();

        List<PostCore> posts = new();

        for (int i = 0; i < SeedLimit; i++)
        {
            posts.Add(new PostCore());
        }

        //context.Posts.BulkUpdate
        context.SaveChanges();
    }

    [Benchmark]
    public void EfCoreBulkUpdateZzzEfExtensions()
    {
        throw new NotImplementedException();

        using var context = _corePooledDbContextFactory.CreateDbContext();

        List<PostCore> posts = new();

        for (int i = 0; i < SeedLimit; i++)
        {
            posts.Add(PostCore.NewPost());
        }

        context.BulkInsert(posts);
    }
}
