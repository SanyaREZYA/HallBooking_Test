public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;
    private readonly IBookingPricingService _pricingService;
    private readonly IHallService _hallService;
    private readonly IHallOptionService _hallOptionService;

    public BookingService(IBookingRepository repository, IBookingPricingService pricingService, IHallService hallService, IHallOptionService hallOptionService)
    {
        _repository = repository;
        _pricingService = pricingService;
        _hallService = hallService;
        _hallOptionService = hallOptionService;
    }

    public async Task<List<Booking>> GetAllBookingAsync()
        => await _repository.GetAllBookingAsync();

    public async Task<Booking?> GetBookingByIdAsync(int id)
        => await _repository.GetBookingByIdAsync(id);

    public async Task<List<Booking>> GetBookingByCustomerAsync(int customerId, BookingStatus? status = null)
        => await _repository.GetBookingByCustomerAsync(customerId, status);

    public async Task<List<Booking>> GetBookingByHallAsync(int hallId, BookingStatus? status = null)
        => await _repository.GetBookingByHallAsync(hallId, status);

    public async Task<bool> HasConflictAsync(int hallId, DateTime startTime, DateTime endTime)
        => await _repository.HasConflictAsync(hallId, startTime, endTime);

    public async Task<Booking> CreateBookingAsync(CreateBookingDto dto)
    {
        var hall = await _hallService.GetHallByIdAsync(dto.HallId);

        if (hall is null)
            throw new InvalidOperationException("Hall not found.");

        var hallOptions = new List<HallOption>();

        foreach (var optionId in dto.HallOptionIds)
        {
            var option = await _hallOptionService.GetHallOptionByIdAsync(optionId);

            if (option is null)
                throw new InvalidOperationException($"Hall option with ID {optionId} not found.");

            hallOptions.Add(option);
        }

        var booking = new Booking
        {
            HallId = dto.HallId,
            CustomerId = dto.CustomerId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            TotalPrice = _pricingService.CalculateRentalPrice(hall.HourlyRate, dto.StartTime, dto.EndTime, hallOptions),
            Status = dto.Status
        };

        foreach (var optionId in dto.HallOptionIds)
        {
            booking.BookingHallOptions.Add(new BookingHallOption
            {
                HallOptionId = optionId
            });
        }

        return await _repository.CreateBookingAsync(booking);
    }

    public async Task<Booking?> UpdateBookingAsync(int id, UpdateBookingDto dto)
    {
        var existingBooking = await _repository.GetBookingByIdAsync(id);

        if (existingBooking is null)
            return null;

        var hall = await _hallService.GetHallByIdAsync(dto.HallId);

        if (hall is null)
            throw new InvalidOperationException("Hall not found.");

        var hallOptions = new List<HallOption>();

        foreach (var optionId in dto.HallOptionIds)
        {
            var option = await _hallOptionService.GetHallOptionByIdAsync(optionId);

            if (option is null)
                throw new InvalidOperationException(
                    $"Hall option with ID {optionId} not found.");

            hallOptions.Add(option);
        }

        existingBooking.HallId = dto.HallId;
        existingBooking.StartTime = dto.StartTime;
        existingBooking.EndTime = dto.EndTime;
        existingBooking.TotalPrice = _pricingService.CalculateRentalPrice(hall.HourlyRate, dto.StartTime, dto.EndTime, hallOptions);
        existingBooking.BookingHallOptions.Clear();

        foreach (var optionId in dto.HallOptionIds)
        {
            existingBooking.BookingHallOptions.Add(new BookingHallOption
            {
                BookingId = existingBooking.Id,
                HallOptionId = optionId
            });
        }

        return await _repository.UpdateBookingAsync(existingBooking);
    }

    public async Task<Booking?> UpdateBookingStatusAsync(int id, BookingStatus status)
        => await _repository.UpdateBookingStatusAsync(id, status);

    public async Task<bool> DeleteBookingAsync(int id)
        => await _repository.DeleteBookingAsync(id);
}