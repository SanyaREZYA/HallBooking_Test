using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Booking>> GetAllBookingAsync()
        => await _context.Bookings.ToListAsync();

    public async Task<Booking?> GetBookingByIdAsync(int id)
        => await _context.Bookings.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Booking>> GetBookingByCustomerAsync(int customerId, BookingStatus? status = null)
    {
        var query = _context.Bookings
                .Where(b => b.CustomerId == customerId);

        if (status.HasValue)
        {
            query = query.Where(b => b.Status == status.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<List<Booking>> GetBookingByHallAsync(int hallId, BookingStatus? status = null)
    {
        var query = _context.Bookings
                .Where(b => b.HallId == hallId);

        if (status.HasValue)
        {
            query = query.Where(b => b.Status == status.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> HasConflictAsync(int hallId, DateTime startTime, DateTime endTime)
        => await _context.Bookings.AnyAsync(b =>
            b.HallId == hallId && b.Status != BookingStatus.Cancelled &&
            b.StartTime < endTime && b.EndTime > startTime);

    public async Task<Booking> CreateBookingAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<Booking?> UpdateBookingAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<Booking?> UpdateBookingStatusAsync(int id, BookingStatus status)
    {
        var booking = await GetBookingByIdAsync(id);
        if (booking is null)
            return null;

        booking.Status = status;
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> DeleteBookingAsync(int id)
    {
        var deleted = await GetBookingByIdAsync(id);
        if (deleted is null)
            return false;

        _context.Bookings.Remove(deleted);
        await _context.SaveChangesAsync();
        return true;
    }
}