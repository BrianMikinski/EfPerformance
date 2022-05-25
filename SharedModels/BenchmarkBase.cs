using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Blog.Benchmarks;

[MemoryDiagnoser]
[RPlotExporter]
[MinColumn]
[MaxColumn]
public abstract class BenchmarkBase
{
    protected const string EF_CORE_CATEGORY = "EF Core";
    protected const string EF_6_CATEGORY = "EF 6";

    protected int SeedLimit = 1000;

    protected PooledDbContextFactory<CoreBlogContext> _corePooledDbContextFactory;

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
        using var context = _corePooledDbContextFactory.CreateDbContext();

        var tags = new List<TagCore>()
        {
            TagCore.NewTag(),
            TagCore.NewTag(),
            TagCore.NewTag(),
            TagCore.NewTag()
        };

        context.Tags.AddRange(tags);

        context.SaveChanges(true);

        var categories = new List<CategoryCore>()
        {
            CategoryCore.NewCategory(),
            CategoryCore.NewCategory(),
            CategoryCore.NewCategory()
        };

        context.Categories.AddRange(categories);

        context.SaveChanges(true);


        for (int i = 0; i < SeedLimit; i++)
        {
            var (post, postTags) = PostCore.NewPostWithCategoriesAndTags(categories[0], tags);

            context.Posts.Add(post);
            context.SaveChanges(true);

            context.PostTags.AddRange(postTags);
            context.SaveChanges(true);
        }
    }

    /// <summary>
    /// Ef 6 full database seed
    /// </summary>
    private void FullDatabaseSeedEf6()
    {

    }

    /// <summary>
    /// Configure database setup
    /// </summary>
    public void ConfigDatabases()
    {
        // ef 6
        BlogContext _blogContext = new();

        _blogContext.Database.Delete();
        _blogContext.Database.Create();

        // ef core
        _corePooledDbContextFactory = new PooledDbContextFactory<CoreBlogContext>(CoreBlogContext.NewDbContextOptions());

        CoreBlogContext _coreBlogContext = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());

        _coreBlogContext.Database.EnsureDeleted();
        _coreBlogContext.Database.EnsureCreated();
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddPostsToSeedLimit(int? overridenLimit = null, bool isBulkInsert = false)
    {
        if (overridenLimit.HasValue)
        {
            SeedLimit = overridenLimit.Value;
        }

        if (!isBulkInsert)
        {
            PostsAddEfCore();
            PostsAddEf6();
        }
        else
        {
            PostsAddBulkInsertEfCore(); ;
            PostsAddBulkInsertEf6();
        }

    }

    /// <summary>
    /// EF Core multiple posts using native Add
    /// </summary>
    protected void PostsAddEfCore()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        for (int i = 0; i < SeedLimit; i++)
        {
            var post = PostCore.NewPost();
            context.Posts.Add(post);
            context.SaveChanges();
        }
    }

    /// <summary>
    /// EF Core multiple posts with bulk insert
    /// </summary>
    protected void PostsAddBulkInsertEfCore(int? overrideLimit = null)
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        var limit = overrideLimit ?? SeedLimit;

        List<PostCore> posts = new();

        for (int i = 0; i < limit; i++)
        {
            posts.Add(PostCore.NewPost());
        }

        context.BulkInsert(posts);
    }

    /// <summary>
    /// Add a range of posts to the ef core db context
    /// </summary>
    /// <param name="overrideLimit"></param>
    protected void PostsAddRangeInsertEFCore(int? overrideLimit = null)
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        var limit = overrideLimit ?? SeedLimit;

        List<PostCore> posts = new();

        for (int i = 0; i < limit; i++)
        {
            posts.Add(new PostCore());
        }

        context.Posts.AddRange(posts);
        context.SaveChanges();
    }

    /// <summary>
    /// EF6 multiple post with native add
    /// </summary>
    protected void PostsAddEf6()
    {
        using var context = new BlogContext();

        for (int i = 0; i < SeedLimit; i++)
        {
            var post = Post.NewPost();

            context.Posts.Add(post);
            context.SaveChanges();
        }
    }

    /// <summary>
    /// EF6 multiple psots add
    /// </summary>
    protected void PostsAddBulkInsertEf6(int? overrideLimit = null)
    {
        using var context = new BlogContext();

        var limit = overrideLimit ?? SeedLimit;

        List<Post> posts = new();

        for (int i = 0; i < limit; i++)
        {
            posts.Add(Post.NewPost());
        }

        context.BulkInsert(posts);
    }

    public void PostInsertNoChangeTracker()
    {
        //bool changeTracking = false
    }

    public (BlogDataDiagnostics coreBlog, BlogDataDiagnostics blog) TableDiagnostics()
    {
        using var coreContext = _corePooledDbContextFactory.CreateDbContext();

        var coreBlogDiagnostics = new BlogDataDiagnostics()
        {
            PostsCount = coreContext.Posts.Count(),
            CategoriesCount = coreContext.Categories.Count(),
            TagsCount = coreContext.Tags.Count(),
            PostTagsCount = coreContext.PostTags.Count()
        };

        using var context = new BlogContext();

        var blogDiagnostics = new BlogDataDiagnostics()
        {
            PostsCount = context.Posts.Count(),
            CategoriesCount = context.Categories.Count(),
            TagsCount = context.Tags.Count(),
            PostTagsCount = context.PostTags.Count()
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

    private void EfCoreCleanup()
    {
        using var context = _corePooledDbContextFactory.CreateDbContext();

        context
            .Database
            .ExecuteSqlRaw("delete from PostTags where 1=1");

        context
            .Database
            .ExecuteSqlRaw("delete from Posts where 1=1");

        context
            .Database
            .ExecuteSqlRaw("delete from Categories where 1=1");

        context
            .Database
            .ExecuteSqlRaw("delete from Tags where 1=1");
    }

    private void Ef6Cleanup()
    {
        using var context = new BlogContext();

        context
            .Database.ExecuteSqlCommand("delete from PostTags where 1=1");

        context
            .Database.ExecuteSqlCommand("delete from Posts where 1=1");

        context
            .Database.ExecuteSqlCommand("delete from Categories where 1=1");

        context
            .Database.ExecuteSqlCommand("delete from Tags where 1=1");
    }
}
