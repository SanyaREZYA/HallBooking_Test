public class HallService : IHallService
{
    private readonly IHallRepository _repository;

    public HallService(IHallRepository repository)
    {
        _repository = repository;
    }

    public async Task<Hall?> GetHallByIdAsync(int id)
        => await _repository.GetHallByIdAsync(id);

    public async Task<List<Hall>> GetAllHallsAsync()
        => await _repository.GetAllHallsAsync();

    public async Task<Hall> CreateHallAsync(HallDto dto)
    {
        var hall = new Hall
        {
            Name = dto.Name,
            Capacity = dto.Capacity,
            HourlyRate = dto.HourlyRate,
            IsActive = true
        };

        return await _repository.CreateHallAsync(hall);
    }

    public async Task<Hall?> UpdateHallAsync(int id, HallDto dto)
    {
        var existingHall = await _repository.GetHallByIdAsync(id);

        if (existingHall is null)
        {
            return null;
        }

        existingHall.Name = dto.Name;
        existingHall.Capacity = dto.Capacity;
        existingHall.HourlyRate = dto.HourlyRate;
        existingHall.IsActive = dto.IsActive;

        return await _repository.UpdateHallAsync(existingHall);
    }

    public async Task<bool> DeleteHallAsync(int id)
        => await _repository.DeleteHallAsync(id);

    public async Task<List<Hall>> GetAvailableAsync(DateTime startTime, DateTime endTime, int capacity)
    {
        if (startTime >= endTime)
        {
            throw new ArgumentException("Start time must be earlier than end time.");
        }

        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than zero.");
        }

        return await _repository.GetAvailableAsync(startTime, endTime, capacity);
    }
}