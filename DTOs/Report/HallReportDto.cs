public class HallReportDto
{
    public int HallId { get; set; }
    public string HallName { get; set; } = null!;
    public int BookingCount { get; set; }
    public int BookingHours { get; set; }
    public decimal Revenue { get; set; }

}