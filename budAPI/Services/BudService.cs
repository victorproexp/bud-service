using budAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace budAPI.Services;

public class BudService
{
    private readonly IMongoCollection<Bud> _BudCollection;

    public BudService(
        IOptions<BudDatabaseSettings> BudDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            BudDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            BudDatabaseSettings.Value.DatabaseName);

        _BudCollection = mongoDatabase.GetCollection<Bud>(
            BudDatabaseSettings.Value.BudCollectionName);
    }

    public async Task<List<Bud>> GetAsync() =>
        await _BudCollection.Find(_ => true).ToListAsync();

    public async Task<Bud?> GetAsync(string id) =>
        await _BudCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Bud newBud) =>
        await _BudCollection.InsertOneAsync(newBud);

    public async Task UpdateAsync(string id, Bud updatedBud) =>
        await _BudCollection.ReplaceOneAsync(x => x.Id == id, updatedBud);

    public async Task RemoveAsync(string id) =>
        await _BudCollection.DeleteOneAsync(x => x.Id == id);
}