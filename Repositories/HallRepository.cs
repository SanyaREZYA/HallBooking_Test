using Microsoft.EntityFrameworkCore;

public class HallRepository : IHallRepository
{
    private readonly AppDbContext _context;

    public HallRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hall>> GetAllHallsAsync()
    {
        return await _context.Halls.ToListAsync();
    }

    public async Task<Hall?> GetHallByIdAsync(int id)
    {
        return await _context.Halls.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Hall> CreateHallAsync(Hall hall)
    {
        _context.Halls.Add(hall);
        await _context.SaveChangesAsync();
        return hall;
    }

    public async Task<Hall?> UpdateHallAsync(Hall hall)
    {
        _context.Halls.Update(hall);

        await _context.SaveChangesAsync();

        return hall;
    }

    public async Task<bool> DeleteHallAsync(int id)
    {
        var hall = await GetHallByIdAsync(id);
        if (hall is null)
            return false;

        _context.Halls.Remove(hall);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Hall>> GetAvailableAsync(DateTime startTime, DateTime endTime, int capacity)
    {
        return await _context.Halls.Where(h =>
            h.IsActive && h.Capacity >= capacity && !h.Bookings.Any(b =>
                b.Status != BookingStatus.Cancelled && b.StartTime < endTime && b.EndTime > startTime)).ToListAsync();
    }
}