using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HallController : ControllerBase
{
    private readonly IHallService _hallService;

    public HallController(IHallService hallService)
    {
        _hallService = hallService;
    }

    [HttpGet("halls")]
    public async Task<ActionResult<List<Hall>>> GetAllHalls()
    {
        var halls = await _hallService.GetAllHallsAsync();
        if (halls == null)
        {
            return NotFound();
        }
        return Ok(halls);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hall>> GetHallById(int id)
    {
        var hall = await _hallService.GetHallByIdAsync(id);
        if (hall == null)
        {
            return NotFound();
        }
        return Ok(hall);
    }

    [HttpPost]
    public async Task<ActionResult<Hall>> CreateHall(HallDto dto)
    {
        var hall = await _hallService.CreateHallAsync(dto);
        return CreatedAtAction(nameof(GetHallById), new { id = hall.Id }, hall);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Hall>> UpdateHall(int id, HallDto dto)
    {
        var updatedHall = await _hallService.UpdateHallAsync(id, dto);
        if (updatedHall == null)
        {
            return NotFound();
        }
        return Ok(updatedHall);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHallById(int id)
    {
        var deleted = await _hallService.DeleteHallAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("available")]
    public async Task<ActionResult<List<Hall>>> GetAvailable([FromQuery] DateTime startTime, [FromQuery] DateTime endTime, [FromQuery] int capacity)
    {
        var halls = await _hallService.GetAvailableAsync(startTime, endTime, capacity);
        return Ok(halls);
    }
}
