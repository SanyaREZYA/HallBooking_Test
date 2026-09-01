public class HallOptionService : IHallOptionService
{
    private readonly IHallOptionRepository _repository;

    public HallOptionService(IHallOptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<HallOption?> GetHallOptionByIdAsync(int id)
        => await _repository.GetHallOptionByIdAsync(id);

    public async Task<List<HallOption>> GetAllHallOptionsAsync()
        => await _repository.GetAllHallOptionsAsync();

    public async Task<HallOption> CreateHallOptionAsync(HallOptionDto dto)
    {
        var hallOption = new HallOption
        {
            Name = dto.Name,
            Price = dto.Price,
            IsActive = dto.IsActive
        };
        return await _repository.CreateHallOptionAsync(hallOption);
    }

    public async Task<HallOption?> UpdateHallOptionAsync(int id, HallOptionDto dto)
    {
        var existingHallOption = await _repository.GetHallOptionByIdAsync(id);

        if (existingHallOption is null)
        {
            return null;
        }

        existingHallOption.Name = dto.Name;
        existingHallOption.Price = dto.Price;
        existingHallOption.IsActive = dto.IsActive;

        return await _repository.UpdateHallOptionAsync(existingHallOption);
    }

    public async Task<bool> DeleteHallOptionAsync(int id)
        => await _repository.DeleteHallOptionAsync(id);
}