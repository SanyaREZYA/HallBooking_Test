public class UpdateBookingDto
{
    public int HallId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
    public List<int> HallOptionIds { get; set; } = new();
}