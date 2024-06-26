using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TickerSubscription.Settings;

namespace TickerSubscription.Repo;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public Repository(IOptions<TickerStoreSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<T>(settings.Value.CollectionName);
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task<IReadOnlyCollection<T>> GetAllForPeriodAsync(DateTime startTimestamp, DateTime endTimestamp)
    {
        var filterBuilder = Builders<T>.Filter;
        var filter = filterBuilder.Gte("timestamp", startTimestamp) & filterBuilder.Lte("timestamp", endTimestamp);

        return await _collection.Find(filter).ToListAsync();
    }
}