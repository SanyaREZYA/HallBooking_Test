public interface IHallService
{
    Task<Hall?> GetHallByIdAsync(int id);
    Task<List<Hall>> GetAllHallsAsync();
    Task<Hall> CreateHallAsync(HallDto dto);
    Task<Hall?> UpdateHallAsync(int id, HallDto dto);
    Task<bool> DeleteHallAsync(int id);
    Task<List<Hall>> GetAvailableAsync(DateTime startTime, DateTime endTime, int capacity);
}