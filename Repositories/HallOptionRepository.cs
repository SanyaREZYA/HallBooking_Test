using Microsoft.EntityFrameworkCore;

public class HallOptionRepository : IHallOptionRepository
{
    private readonly AppDbContext _context;

    public HallOptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<HallOption>> GetAllHallOptionsAsync()
        => await _context.HallOptions.ToListAsync();

    public async Task<HallOption?> GetHallOptionByIdAsync(int id)
        => await _context.HallOptions.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<HallOption> CreateHallOptionAsync(HallOption hallOption)
    {
        _context.HallOptions.Add(hallOption);
        await _context.SaveChangesAsync();
        return hallOption;
    }

    public async Task<HallOption?> UpdateHallOptionAsync(HallOption hallOption)
    {
        _context.HallOptions.Update(hallOption);
        await _context.SaveChangesAsync();
        return hallOption;
    }

    public async Task<bool> DeleteHallOptionAsync(int id)
    {
        var hallOption = await GetHallOptionByIdAsync(id);
        if (hallOption is null)
            return false;

        _context.HallOptions.Remove(hallOption);
        await _context.SaveChangesAsync();
        return true;
    }
}