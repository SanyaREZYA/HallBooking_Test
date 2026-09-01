public interface IHallOptionService
{
    Task<HallOption?> GetHallOptionByIdAsync(int id);
    Task<List<HallOption>> GetAllHallOptionsAsync();
    Task<HallOption> CreateHallOptionAsync(HallOptionDto dto);
    Task<HallOption?> UpdateHallOptionAsync(int id, HallOptionDto dto);
    Task<bool> DeleteHallOptionAsync(int id);
}