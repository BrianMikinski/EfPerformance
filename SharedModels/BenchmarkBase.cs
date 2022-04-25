using BenchmarkDotNet.Attributes;
using Blog.Models;
using CoreBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Blog.Benchmarks;

[MinColumn]
[MaxColumn]
public abstract class BenchmarkBase
{
    protected const string EF_CORE_CATEGORY = "EF Core";
    protected const string EF_6_CATEGORY = "EF 6";

    protected CoreBlogContext _coreBlogContext;
    protected readonly CoreBlogContext _coreBlogContextSingleton;
    protected CoreBlogContext _coreBlogContextPooled;
   
    protected BlogContext _blogContext;
    protected readonly BlogContext _blogContextSingleton;
   

    protected int SeedLimit = 1000;

    private readonly PooledDbContextFactory<CoreBlogContext> _coreDbContextFactory;

    public BenchmarkBase()
    {
        _coreDbContextFactory = new PooledDbContextFactory<CoreBlogContext>(CoreBlogContext.NewDbContextOptions());

        _coreBlogContext = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());
        _coreBlogContextPooled = _coreDbContextFactory.CreateDbContext();
        _coreBlogContextSingleton = _coreDbContextFactory.CreateDbContext();

        _blogContext = new();
        _blogContextSingleton = new();
    }

    /// <summary>
    /// Create new db contexts for benchmarks
    /// </summary>
    protected void NewDbContexts()
    {
        _coreBlogContext = new CoreBlogContext(CoreBlogContext.NewDbContextOptions());
        _coreBlogContextPooled = _coreDbContextFactory.CreateDbContext();
        
        _blogContext = new BlogContext();
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
        for (int i = 0; i < SeedLimit; i++)
        {
            var post = PostCore.NewPost();
            _coreBlogContext.Posts.Add(post);
            _coreBlogContext.SaveChanges();
        }
    }

    /// <summary>
    /// EF Core multiple posts with bulk insert
    /// </summary>
    protected void PostsAddBulkInsertEfCore(int? overrideLimit = null)
    {
        var limit = overrideLimit ?? SeedLimit;

        List<PostCore> posts = new();

        for (int i = 0; i < limit; i++)
        {
            posts.Add(PostCore.NewPost());
        }

        _coreBlogContext.BulkInsert(posts);
    }

    /// <summary>
    /// Add a range of posts to the ef core db context
    /// </summary>
    /// <param name="overrideLimit"></param>
    protected void PostsAddRangeInsertEFCore(int? overrideLimit = null)
    {
        var limit = overrideLimit ?? SeedLimit;

        List<PostCore> posts = new();

        for (int i = 0; i < limit; i++)
        {
            posts.Add(new PostCore());
        }

        _coreBlogContext.Posts.AddRange(posts);
        _coreBlogContext.SaveChanges();
    }

    /// <summary>
    /// EF6 multiple post with native add
    /// </summary>
    protected void PostsAddEf6()
    {
        for (int i = 0; i < SeedLimit; i++)
        {
            var post = Post.NewPost();

            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();
        }
    }

    /// <summary>
    /// EF6 multiple psots add
    /// </summary>
    protected void PostsAddBulkInsertEf6(int? overrideLimit = null)
    {
        var limit = overrideLimit.HasValue ? overrideLimit.Value : SeedLimit;

        List<Post> posts = new();

        for (int i = 0; i < limit; i++)
        {
            posts.Add(Post.NewPost());
        }

        _blogContext.BulkInsert(posts);
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
