public class BookingPricingService : IBookingPricingService
{
    public decimal CalculateRentalPrice(decimal hourlyRate, DateTime startTime, DateTime endTime, List<HallOption> hallOptions)
    {
        if (hourlyRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(hourlyRate));

        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.");

        if ((endTime - startTime).TotalHours % 1 != 0)
            throw new ArgumentException("Booking duration must be a whole number of hours.");

        decimal totalPrice = 0m;

        var currentTime = startTime;

        while (currentTime < endTime)
        {
            var multiplier = GetHourlyMultiplier(currentTime.TimeOfDay);

            totalPrice += hourlyRate * multiplier;

            currentTime = currentTime.AddHours(1);
        }
        totalPrice += GetHallOptionsTotalPrice(hallOptions);

        return totalPrice;
    }

    private decimal GetHallOptionsTotalPrice(List<HallOption> hallOptions)
    {
        decimal totalPrice = 0m;
        foreach (var option in hallOptions)
        {
            totalPrice += option.Price;
        }
        return totalPrice;
    }

    private decimal GetHourlyMultiplier(TimeSpan time)
    {
        if (time >= TimeSpan.FromHours(6) &&
            time < TimeSpan.FromHours(9))
        {
            return 0.90m;
        }

        if (time >= TimeSpan.FromHours(12) &&
            time < TimeSpan.FromHours(14))
        {
            return 1.15m;
        }

        if (time >= TimeSpan.FromHours(18) &&
            time < TimeSpan.FromHours(23))
        {
            return 0.80m;
        }

        return 1.00m;
    }
}