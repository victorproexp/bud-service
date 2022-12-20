using Microsoft.AspNetCore.Mvc;

namespace BudAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudController : ControllerBase
{
    private readonly ILogger<BudController> _logger;

    private readonly IBudService _budService;

    public BudController(ILogger<BudController> logger, IBudService budService)
    {
        _logger = logger;
        _logger.LogDebug(1, "NLog injected into BudController");
        _budService = budService;
    }

    [HttpPost]
    public BudDto Post(BudDto bud)
    {
        _logger.LogInformation("Post called at {DT}",
            DateTime.UtcNow.ToLongTimeString());

        var res = _budService.Send(bud);

        if (res.IsFaulted)
        {
            return null!;
        }

        return bud;
    }

    [HttpGet("version")]
    public IEnumerable<string> GetVersion()
    {
        var properties = new List<string>();
        var assembly = typeof(Program).Assembly;
        foreach (var attribute in assembly.GetCustomAttributesData())
        {
            properties.Add($"{attribute.AttributeType.Name} - {attribute.ToString()}");
        }
        return properties;
    }
}
