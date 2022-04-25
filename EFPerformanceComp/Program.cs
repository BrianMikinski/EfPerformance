using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

BenchmarkRunner.Run<Benchmarks>();

public class Benchmarks
{
    private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=EfPerfComp;Trusted_Connection=True;";

    [Params(1, 100, 1000)]
    public int Rows { get; set; }

    private DbContextOptions<EfCoreContext> _efCoreOptions;
    private PooledDbContextFactory<EfCoreContext> _pooledFactory;

    [GlobalSetup]
    public void Setup()
    {
        //using var connection = new SqlConnection("Server=localhost;Database=test;User=SA;Password=Abcd5678;Connect Timeout=60;ConnectRetryCount=0;Trust Server Certificate=true");
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        using var command = new SqlCommand(@"
DROP TABLE IF EXISTS [Posts];
CREATE TABLE [Posts] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(50) NULL,
    [Content] nvarchar(1000) NULL,
    [CategoryId] uniqueidentifier NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id])
);
", connection);
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO [Posts] (Id, Title, Content, CategoryId) VALUES (NEWID(), 'old man and the sea', 'This is a story', NEWID())";
        for (var i = 0; i < Rows; i++)
        {
            command.ExecuteNonQuery();
        }

        var efCoreOptionsBuilder = new DbContextOptionsBuilder<EfCoreContext>();
        efCoreOptionsBuilder.UseSqlServer(ConnectionString);
        _efCoreOptions = efCoreOptionsBuilder.Options;

        _pooledFactory = new PooledDbContextFactory<EfCoreContext>(_efCoreOptions);
    }

    [Benchmark]
    public void EFCore()
    {
        using var context = new EfCoreContext(_efCoreOptions);

        _ = EntityFrameworkQueryableExtensions.AsNoTracking(context.Posts).ToList();
    }

    [Benchmark]
    public void EFCorePooled()
    {
        using var context = _pooledFactory.CreateDbContext();

        _ = EntityFrameworkQueryableExtensions.AsNoTracking(context.Posts).ToList();
    }

    [Benchmark]
    public void EF6()
    {
        using var context = new Ef6Context();

        _ = context
            .Posts
            .AsNoTracking()
            .ToList();
    }

    public class EfCoreContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Post> Posts { get; set; }
    }

    public class Ef6Context : System.Data.Entity.DbContext
    {
        public Ef6Context()
            : base(ConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public System.Data.Entity.DbSet<Post> Posts { get; set; }
    }


    public class Post
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? Title { get; private set; }

        [StringLength(1000)]
        public string? Content { get; set; }

        public Guid? CategoryId { get; set; }
    }
}