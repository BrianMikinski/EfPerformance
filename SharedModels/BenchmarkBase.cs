using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Benchmarks;

[MinColumn]
[MaxColumn]
public class BenchmarkBase
{
    protected CoreBlogContext _coreBlogContext;
    protected BlogContext _blogContext;

    protected int SeedLimit = 1000;

    public BenchmarkBase()
    {
        _coreBlogContext = new ();
        _blogContext = new ();
    }

    /// <summary>
    /// Create new db contexts for benchmarks
    /// </summary>
    protected void NewDbContexts()
    {
        _coreBlogContext = new ();
        _blogContext = new ();
    }

    /// <summary>
    /// Full real word database seed
    /// </summary>
    public void FullDatabaseSeed()
    {
        FullDatabaseSeedEfCore();
        FullDatabaseSeedEf6();
    }

    /// <summary>
    /// EF core full database seed
    /// </summary>
    private void FullDatabaseSeedEfCore()
    {
        var tags = new List<TagCore>()
        {
            TagCore.NewTag(),
            TagCore.NewTag(),
            TagCore.NewTag(),
            TagCore.NewTag()
        };

        _coreBlogContext.Tags.AddRange(tags);

        _coreBlogContext.SaveChanges(true);

        var categories = new List<CategoryCore>()
        {
            CategoryCore.NewCategory(),
            CategoryCore.NewCategory(),
            CategoryCore.NewCategory()
        };

        _coreBlogContext.Categories.AddRange(categories);

        _coreBlogContext.SaveChanges(true);


        for (int i = 0; i < SeedLimit; i++)
        {
            var (post, postTags) = PostCore.NewPostWithCategoriesAndTags(categories[0], tags);

            _coreBlogContext.Posts.Add(post);
            _coreBlogContext.SaveChanges(true);

            _coreBlogContext.PostTags.AddRange(postTags);
            _coreBlogContext.SaveChanges(true);
        }
    }

    /// <summary>
    /// Ef 6 full database seed
    /// </summary>
    private void FullDatabaseSeedEf6()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void AddPostsToSeedLimit()
    {
        PostsAddEfCore();
        PostsAddEf6();
    }

    /// <summary>
    /// EF Core multiple posts add
    /// </summary>
    private void PostsAddEfCore()
    {
        for (int i = 0; i < SeedLimit; i++)
        {
            var post = PostCore.NewPost();

            _coreBlogContext.Posts.Add(post);
            _coreBlogContext.SaveChanges(true);
        }
    }

    /// <summary>
    /// EF6 multiple psots add
    /// </summary>
    private void PostsAddEf6()
    {
        for (int i = 0; i < SeedLimit; i++)
        {
            var post = Post.NewPost();

            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();
        }
    }

    public void PostInsertNoChangeTracker()
    {
        //bool changeTracking = false
    }

    public (BlogDataDiagnostics coreBlog, BlogDataDiagnostics blog) TableDiagnostics()
    {
        var coreBlogDiagnostics = new BlogDataDiagnostics()
        {
            PostsCount = _coreBlogContext.Posts.Count(),
            CategoriesCount = _coreBlogContext.Categories.Count(),
            TagsCount = _coreBlogContext.Tags.Count(),
            PostTagsCount = _coreBlogContext.PostTags.Count()
        };

        var blogDiagnostics = new BlogDataDiagnostics()
        {
            PostsCount = _blogContext.Posts.Count(),
            CategoriesCount = _blogContext.Categories.Count(),
            TagsCount = _blogContext.Tags.Count(),
            PostTagsCount = _blogContext.PostTags.Count()
        };

        return (coreBlogDiagnostics, blogDiagnostics);
    }

    /// <summary>
    /// Cleanup databases
    /// </summary>
    public void BaseCleanup()
    {
        EfCoreCleanup();
        Ef6Cleanup();
    }

    protected void EfCoreCleanup()
    {
        _coreBlogContext
            .Database
            .ExecuteSqlRaw("delete from PostTags where 1=1");

        _coreBlogContext
            .Database
            .ExecuteSqlRaw("delete from Posts where 1=1");

        _coreBlogContext
            .Database
            .ExecuteSqlRaw("delete from Categories where 1=1");

        _coreBlogContext
            .Database
            .ExecuteSqlRaw("delete from Tags where 1=1");
    }

    protected void Ef6Cleanup()
    {
        _blogContext
            .Database.ExecuteSqlCommand("delete from PostTags where 1=1");

        _blogContext
            .Database.ExecuteSqlCommand("delete from Posts where 1=1");

        _blogContext
            .Database.ExecuteSqlCommand("delete from Categories where 1=1");

        _blogContext
            .Database.ExecuteSqlCommand("delete from Tags where 1=1");
    }
}
