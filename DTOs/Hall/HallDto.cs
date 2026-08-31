using System.ComponentModel.DataAnnotations;
public class HallDto
{
    [Required(ErrorMessage = "Name is required")]
    public String Name { get; set; } = null!;

    [Required(ErrorMessage = "Capacity is required")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Hourly rate is required")]
    public decimal HourlyRate { get; set; }

    public bool IsActive { get; set; } = true;
}