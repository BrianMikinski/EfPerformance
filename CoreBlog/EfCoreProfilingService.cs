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
            var (post, postTags) = Post.NewPost(categories[0], tags);

            _coreBlogContext.Posts.Add(post);
            _coreBlogContext.SaveChanges(true);

            _coreBlogContext.PostTags.AddRange(postTags);
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
        _coreBlogContext
            .Database.ExecuteSqlRaw("delete from PostTags where 1=1");

        _coreBlogContext
            .Database.ExecuteSqlRaw("delete from Posts where 1=1");

        _coreBlogContext
            .Database.ExecuteSqlRaw("delete from Categories where 1=1");

        _coreBlogContext
            .Database.ExecuteSqlRaw("delete from Tags where 1=1");
    }

    [IterationCleanup]
    public void IterationCleanup()
    {

    }
}