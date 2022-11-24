using budAPI.Models;

namespace budAPI.Services;

public interface IBudService
{
    Task<List<Bud>> GetAsync();
    Task<Bud?> GetAsync(string id);
    Task CreateAsync(Bud newBud);
    Task UpdateAsync(string id, Bud updatedBud);
    Task RemoveAsync(string id);
}