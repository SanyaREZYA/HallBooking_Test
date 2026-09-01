using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HallOptionController : ControllerBase
{
    private readonly IHallOptionService _hallOptionService;

    public HallOptionController(IHallOptionService hallOptionService)
    {
        _hallOptionService = hallOptionService;
    }

    [HttpGet("hall-options")]
    public async Task<ActionResult<List<HallOption>>> GetAllHallOptions()
    {
        var hallOptions = await _hallOptionService.GetAllHallOptionsAsync();
        if (hallOptions == null)
        {
            return NotFound();
        }
        return Ok(hallOptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HallOption>> GetHallOptionById(int id)
    {
        var hallOptions = await _hallOptionService.GetHallOptionByIdAsync(id);
        if (hallOptions == null)
        {
            return NotFound();
        }
        return Ok(hallOptions);
    }

    [HttpPost]
    public async Task<ActionResult<HallOption>> CreateHallOption(HallOptionDto dto)
    {
        var hallOptions = await _hallOptionService.CreateHallOptionAsync(dto);
        return CreatedAtAction(nameof(GetHallOptionById), new { id = hallOptions.Id }, hallOptions);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<HallOption>> UpdateHallOption(int id, HallOptionDto dto)
    {
        var updatedHallOptions = await _hallOptionService.UpdateHallOptionAsync(id, dto);
        if (updatedHallOptions == null)
        {
            return NotFound();
        }
        return Ok(updatedHallOptions);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCHallOptionById(int id)
    {
        var deleted = await _hallOptionService.DeleteHallOptionAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}