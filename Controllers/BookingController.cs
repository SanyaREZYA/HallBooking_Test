using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("bookings")]
    public async Task<ActionResult<List<Booking>>> GetAllBooking()
    {
        var bookings = await _bookingService.GetAllBookingAsync();
        if (bookings == null)
        {
            return NotFound();
        }
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetBookingById(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<Booking>>> GetBookingByCustomer(int customerId, BookingStatus? status = null)
    {
        var bookings = await _bookingService.GetBookingByCustomerAsync(customerId, status);

        if (bookings == null)
        {
            return NotFound();
        }

        return Ok(bookings);
    }

    [HttpGet("hall/{hallId}")]
    public async Task<ActionResult<List<Booking>>> GetBookingByHall(int hallId, BookingStatus? status = null)
    {
        var bookings = await _bookingService.GetBookingByHallAsync(hallId, status);

        if (bookings == null)
        {
            return NotFound();
        }

        return Ok(bookings);
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> CreateBooking(CreateBookingDto dto)
    {
        var booking = await _bookingService.CreateBookingAsync(dto);
        return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Booking>> UpdateBooking(int id, UpdateBookingDto dto)
    {
        var updatedBooking = await _bookingService.UpdateBookingAsync(id, dto);
        if (updatedBooking == null)
        {
            return NotFound();
        }
        return Ok(updatedBooking);
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult<Booking>> UpdateBookingStatus(int id, BookingStatus status)
    {
        var updatedBooking = await _bookingService.UpdateBookingStatusAsync(id, status);

        if (updatedBooking is null)
        {
            return NotFound();
        }

        return Ok(updatedBooking);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookingById(int id)
    {
        var deleted = await _bookingService.DeleteBookingAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
