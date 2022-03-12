using BenchmarkDotNet.Attributes;
using CoreBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreBlog;

[MinColumn]
[MaxColumn]
public class EfCoreProfilingService
{
    private CoreBlogContext _coreBlogContext;

    private int SeedLimit = 1000;

    public EfCoreProfilingService()
    {
        _coreBlogContext = new CoreBlogContext();
    }

    public void SeedManyPostsData()
    {
        var tags = new List<Tag>()
        {
            Tag.NewTag(),
            Tag.NewTag(),
            Tag.NewTag(),
            Tag.NewTag()
        };

        _coreBlogContext.Tags.AddRange(tags);

        _coreBlogContext.SaveChanges(true);

        var categories = new List<Category>()
        {
            Category.NewCategory(),
            Category.NewCategory(),
            Category.NewCategory()
        };

        _coreBlogContext.Categories.AddRange(categories);

        _coreBlogContext.SaveChanges(true);


        for(int i = 0; i < SeedLimit; i++)
        {
            var post = Post.NewPost(categories[0], tags);

            _coreBlogContext.Posts.Add(post);
            _coreBlogContext.SaveChanges(true);
        }
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
        _coreBlogContext.Tags.FromSqlRaw("TRUNCATE TABLE [PostTags]");
        _coreBlogContext.Tags.FromSqlRaw("TRUNCATE TABLE [Posts]");
        _coreBlogContext.Tags.FromSqlRaw("TRUNCATE TABLE [Categories]");
        _coreBlogContext.Tags.FromSqlRaw("TRUNCATE TABLE [Tags]");
    }

    [IterationCleanup]
    public void IterationCleanup()
    {

    }
}