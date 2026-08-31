using System.ComponentModel.DataAnnotations;
public class CustomerDto
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;
}