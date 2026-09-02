public interface IBookingRepository
{
    Task<List<Booking>> GetAllBookingAsync();
    Task<Booking?> GetBookingByIdAsync(int id);
    Task<List<Booking>> GetBookingByCustomerAsync(int customerId, BookingStatus? status = null);
    Task<List<Booking>> GetBookingByHallAsync(int hallId, BookingStatus? status = null);
    Task<bool> HasConflictAsync(int hallId, DateTime startTime, DateTime endTime);
    Task<Booking> CreateBookingAsync(Booking booking);
    Task<Booking?> UpdateBookingAsync(Booking booking);
    Task<Booking?> UpdateBookingStatusAsync(int id, BookingStatus status);
    Task<bool> DeleteBookingAsync(int id);
}