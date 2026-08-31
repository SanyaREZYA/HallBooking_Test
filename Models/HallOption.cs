public class HallOption
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<BookingHallOption> BookingHallOption { get; set; } = new List<BookingHallOption>();
}