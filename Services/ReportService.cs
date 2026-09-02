public class ReportService : IReportService
{
    private readonly IReportRepository _repository;

    public ReportService(IReportRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<CustomerReportDto>> GetCustomerReportAsync(DateTime? from = null, DateTime? to = null)
        => await _repository.GetCustomerReportAsync(from, to);
    public async Task<List<HallOptionReportDto>> GetHallOptionReportAsync(DateTime? from = null, DateTime? to = null)
        => await _repository.GetHallOptionReportAsync(from, to);
    public async Task<List<HallReportDto>> GetHallReportAsync(DateTime? from = null, DateTime? to = null)
        => await _repository.GetHallReportAsync(from, to);
}