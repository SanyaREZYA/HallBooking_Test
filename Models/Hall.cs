public class Hall
{
    public int Id { get; set; }

    public String Name { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal HourlyRate { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}