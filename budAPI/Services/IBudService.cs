using budAPI.Models;

namespace budAPI.Services;

public interface IBudService
{
    Task Send(Bud bud);
}