using budAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace budAPI.Services;

public class BudService : IBudService
{
    private readonly IMongoCollection<Bud> _budCollection;

    private readonly IVaultService _vaultService;

    public BudService(
        IOptions<BudDatabaseSettings> budDatabaseSettings, IVaultService vaultService)
    {
        _vaultService = vaultService;
        var mongoClient = new MongoClient(vaultService.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            budDatabaseSettings.Value.DatabaseName);

        _budCollection = mongoDatabase.GetCollection<Bud>(
            budDatabaseSettings.Value.BudCollectionName);
    }

    public async Task<List<Bud>> GetAsync() =>
        await _budCollection.Find(_ => true).ToListAsync();

    public async Task<Bud?> GetAsync(string id) =>
        await _budCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Bud newBud) =>
        await _budCollection.InsertOneAsync(newBud);

    public async Task UpdateAsync(string id, Bud updatedBud) =>
        await _budCollection.ReplaceOneAsync(x => x.Id == id, updatedBud);

    public async Task RemoveAsync(string id) =>
        await _budCollection.DeleteOneAsync(x => x.Id == id);
}