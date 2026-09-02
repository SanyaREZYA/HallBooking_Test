using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("customers")]
    public async Task<ActionResult<List<CustomerReportDto>>> GetCustomerReport(DateTime? from = null, DateTime? to = null)
    {
        var report = await _reportService.GetCustomerReportAsync(from, to);

        return Ok(report);
    }

    [HttpGet("halls")]
    public async Task<ActionResult<List<HallReportDto>>> GetHallReport(DateTime? from = null, DateTime? to = null)
    {
        var report = await _reportService.GetHallReportAsync(from, to);

        return Ok(report);
    }

    [HttpGet("hall-options")]
    public async Task<ActionResult<List<HallOptionReportDto>>> GetHallOptionReport(DateTime? from = null, DateTime? to = null)
    {
        var report = await _reportService.GetHallOptionReportAsync(from, to);

        return Ok(report);
    }
}