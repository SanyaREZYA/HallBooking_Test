public interface IBookingPricingService
{
    decimal CalculateRentalPrice(decimal hourlyRate, DateTime startTime, DateTime endTime, List<HallOption> hallOptions);
}