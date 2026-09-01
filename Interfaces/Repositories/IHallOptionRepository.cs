public interface IHallOptionRepository
{
    Task<List<HallOption>> GetAllHallOptionsAsync();

    Task<HallOption?> GetHallOptionByIdAsync(int id);

    Task<HallOption> CreateHallOptionAsync(HallOption hallOption);

    Task<HallOption?> UpdateHallOptionAsync(HallOption hallOption);

    Task<bool> DeleteHallOptionAsync(int id);
}