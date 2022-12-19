namespace budAPI.Services;

public interface IBudService
{
    Task Send(BudDto budDto);
}