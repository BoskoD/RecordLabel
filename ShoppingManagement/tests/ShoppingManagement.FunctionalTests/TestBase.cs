namespace ShoppingManagement.FunctionalTests;

using ShoppingManagement.Databases;
using ShoppingManagement;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

public class TestBase
{
    public static IConfiguration _configuration;
    public static IServiceScopeFactory _scopeFactory;
    public static WebApplicationFactory<Startup> _factory;
    public static HttpClient _client;

    [SetUp]
    public void TestSetUp()
    {
        _factory = new TestingWebApplicationFactory();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions());
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ShoppingDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ShoppingDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ShoppingDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            await action(scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ShoppingDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            var result = await action(scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();

            return result;
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public static Task ExecuteDbContextAsync(Func<ShoppingDbContext, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<ShoppingDbContext>()));

    public static Task ExecuteDbContextAsync(Func<ShoppingDbContext, ValueTask> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<ShoppingDbContext>()).AsTask());

    public static Task ExecuteDbContextAsync(Func<ShoppingDbContext, IMediator, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<ShoppingDbContext>(), sp.GetService<IMediator>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<ShoppingDbContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<ShoppingDbContext>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<ShoppingDbContext, ValueTask<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<ShoppingDbContext>()).AsTask());

    public static Task<T> ExecuteDbContextAsync<T>(Func<ShoppingDbContext, IMediator, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<ShoppingDbContext>(), sp.GetService<IMediator>()));

    public static Task<int> InsertAsync<T>(params T[] entities) where T : class
    {
        return ExecuteDbContextAsync(db =>
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Add(entity);
            }
            return db.SaveChangesAsync();
        });
    }
}