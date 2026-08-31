using System.ComponentModel.DataAnnotations;
public class HallOptionDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;
}