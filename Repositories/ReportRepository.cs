using Microsoft.EntityFrameworkCore;
public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<HallReportDto>> GetHallReportAsync(DateTime? from = null, DateTime? to = null)
    {
        var query = _context.Bookings
            .Where(b => b.Status != BookingStatus.Cancelled);

        if (from.HasValue)
        {
            query = query.Where(b => b.StartTime >= from.Value);
        }

        if (to.HasValue)
        {
            query = query.Where(b => b.StartTime < to.Value);
        }

        return await query
            .GroupBy(b => new
            {
                b.HallId,
                b.Hall.Name
            })
            .Select(g => new HallReportDto
            {
                HallId = g.Key.HallId,
                HallName = g.Key.Name,
                BookingCount = g.Count(),
                BookingHours = g.Sum(b =>
                    (int)(b.EndTime - b.StartTime).TotalHours),
                Revenue = (int)g.Sum(b => b.TotalPrice)
            })
            .OrderByDescending(x => x.Revenue)
            .ToListAsync();
    }

    public async Task<List<HallOptionReportDto>> GetHallOptionReportAsync(DateTime? from = null, DateTime? to = null)
    {
        var query = _context.BookingHallOptions
            .Where(x => x.Booking.Status != BookingStatus.Cancelled);

        if (from.HasValue)
        {
            query = query.Where(x =>
                x.Booking.StartTime >= from.Value);
        }

        if (to.HasValue)
        {
            query = query.Where(x =>
                x.Booking.StartTime < to.Value);
        }

        return await query
            .GroupBy(x => x.HallOption.Name)
            .Select(g => new HallOptionReportDto
            {
                Name = g.Key,
                UsageCount = g.Count()
            })
            .OrderByDescending(x => x.UsageCount)
            .ToListAsync();
    }

    public async Task<List<CustomerReportDto>> GetCustomerReportAsync(DateTime? from = null, DateTime? to = null)
    {
        var totalCustomers = await _context.Customers.CountAsync();

        var newCustomersQuery = _context.Customers.AsQueryable();

        if (from.HasValue)
        {
            newCustomersQuery = newCustomersQuery
                .Where(c => c.CreatedAt >= from.Value);
        }

        if (to.HasValue)
        {
            newCustomersQuery = newCustomersQuery
                .Where(c => c.CreatedAt < to.Value);
        }

        var newCustomers = await newCustomersQuery.CountAsync();

        var bookingCustomersQuery = _context.Bookings
            .Where(b => b.Status != BookingStatus.Cancelled);

        if (from.HasValue)
        {
            bookingCustomersQuery = bookingCustomersQuery
                .Where(b => b.StartTime >= from.Value);
        }

        if (to.HasValue)
        {
            bookingCustomersQuery = bookingCustomersQuery
                .Where(b => b.StartTime < to.Value);
        }

        var bookingCustomers = await bookingCustomersQuery
            .Select(b => b.CustomerId)
            .Distinct()
            .CountAsync();

        return new List<CustomerReportDto>
    {
        new()
        {
            TotalCustomers = totalCustomers,
            NewCustomers = newCustomers,
            BookingCustomers = bookingCustomers
        }
    };
    }
}