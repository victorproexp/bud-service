namespace budAPI.Models;

public class BudDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string BudCollectionName { get; set; } = null!;
}