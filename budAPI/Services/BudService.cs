using System.Text.Json;

namespace budAPI.Services;

public class BudService : IBudService
{
    private readonly ILogger<BudService> _logger;
    private readonly IConnection _connection;

    public BudService(ILogger<BudService> logger, IConfiguration configuration)
    {
        _logger = logger;

        var mqhostname = configuration["BudBrokerHost"];

        if (String.IsNullOrEmpty(mqhostname))
        {
            mqhostname = "localhost";
        }

        _logger.LogInformation($"Using host at {mqhostname} for message broker");

        var factory = new ConnectionFactory() { HostName = mqhostname };
        _connection = factory.CreateConnection();
    }

    public Task Send(Bud bud)
    {
        try
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "bud",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var body = JsonSerializer.SerializeToUtf8Bytes(bud);

                channel.BasicPublish(exchange: "",
                                    routingKey: "bud",
                                    basicProperties: null,
                                    body: body);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Task.FromException(ex);
        }

        return Task.CompletedTask;
    }
}
