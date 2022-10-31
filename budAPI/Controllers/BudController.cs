using budAPI.Models;
using budAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudController : ControllerBase
{
    private readonly ILogger<BudController> _logger;

    private readonly BudService _budService;

    public BudController(ILogger<BudController> logger, BudService budService)
    {
        _logger = logger;
        _logger.LogDebug(1, "NLog injected into BudController");
        _budService = budService;
    }

    [HttpGet]
    public async Task<List<Bud>> Get() =>
        await _budService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Bud>> Get(string id)
    {
        var Bud = await _budService.GetAsync(id);

        if (Bud is null)
        {
            return NotFound();
        }

        return Bud;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Bud newBud)
    {
        await _budService.CreateAsync(newBud);

        return CreatedAtAction(nameof(Get), new { id = newBud.Id }, newBud);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Bud updatedBud)
    {
        var Bud = await _budService.GetAsync(id);

        if (Bud is null)
        {
            return NotFound();
        }

        updatedBud.Id = Bud.Id;

        await _budService.UpdateAsync(id, updatedBud);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Bud = await _budService.GetAsync(id);

        if (Bud is null)
        {
            return NotFound();
        }

        await _budService.RemoveAsync(id);

        return NoContent();
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
