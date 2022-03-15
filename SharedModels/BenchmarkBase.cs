using BenchmarkDotNet.Attributes;
//using Blog.Models;
using CoreBlog.Models;

namespace Blog.Benchmarks;

public class BenchmarkBase
{
    protected CoreBlogContext _coreBlogContext;
    //protected BlogContext _blogContext;

    protected int SeedLimit = 1000;

    public BenchmarkBase()
    {
        _coreBlogContext = new CoreBlogContext();
        //_blogContext = new BlogContext();
    }

    //[GlobalSetup]
    public void GlobalSetup()
    {

    }

    [GlobalSetup]
    public void GlobalSetupPostSeed()
    {
        PostsAddCore();
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _coreBlogContext = new CoreBlogContext();
    }


    /// <summary>
    /// Full real word database seed
    /// </summary>
    //[Benchmark]
    public void DatabaseSeed()
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

    //[Benchmark]
    public void PostsAddCore()
    {
        for (int i = 0; i < SeedLimit; i++)
        {
            var post = PostCore.NewPost();

            _coreBlogContext.Posts.Add(post);
            _coreBlogContext.SaveChanges(true);
        }
    }

    public BlogDataDiagnostics TableDiagnostics()
    {
        var dataDiagnostics = new BlogDataDiagnostics()
        {
            PostsCount = _coreBlogContext.Posts.Count(),
            CategoriesCount = _coreBlogContext.Categories.Count(),
            TagsCount = _coreBlogContext.Tags.Count(),
            PostTagsCount = _coreBlogContext.PostTags.Count()
        };

        return dataDiagnostics;
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        //_coreBlogContext
        //    .Database.ExecuteSqlRaw("delete from PostTags where 1=1");

        //_coreBlogContext
        //    .Database.ExecuteSqlRaw("delete from Posts where 1=1");

        //_coreBlogContext
        //    .Database.ExecuteSqlRaw("delete from Categories where 1=1");

        //_coreBlogContext
        //    .Database.ExecuteSqlRaw("delete from Tags where 1=1");
    }
}
