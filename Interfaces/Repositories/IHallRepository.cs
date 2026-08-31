public interface IHallRepository
{
    Task<List<Hall>> GetAllHallsAsync();

    Task<Hall?> GetHallByIdAsync(int id);

    Task<Hall> CreateHallAsync(Hall hall);

    Task<Hall?> UpdateHallAsync(Hall hall);

    Task<bool> DeleteHallAsync(int id);

    Task<List<Hall>> GetAvailableAsync(DateTime startTime, DateTime endTime, int capacity);
}