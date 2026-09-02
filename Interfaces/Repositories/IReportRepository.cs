public interface IReportRepository
{
    Task<List<CustomerReportDto>> GetCustomerReportAsync(DateTime? from = null, DateTime? to = null);
    Task<List<HallOptionReportDto>> GetHallOptionReportAsync(DateTime? from = null, DateTime? to = null);
    Task<List<HallReportDto>> GetHallReportAsync(DateTime? from = null, DateTime? to = null);
}