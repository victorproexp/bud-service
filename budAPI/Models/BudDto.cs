using System.ComponentModel.DataAnnotations;

namespace budAPI.Models;

public class BudDto
{
    [Required(ErrorMessage = "VareId is required")]
    public string? VareId { get; set; }

    [Required(ErrorMessage = "KundeId is required")]
    public string? KundeId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Value { get; set; }
}
