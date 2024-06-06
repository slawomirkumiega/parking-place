using Microsoft.AspNetCore.Mvc;
using ParkingPlace.Modules.ParkingSpaces.Core.Services;
using ParkingPlace.Modules.ParkingSpaces.Shared.DTO;

namespace ParkingPlace.Modules.ParkingSpaces.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingSpaceReservationsController : ControllerBase
    {
        private readonly IParkingSpaceReservationService _parkingSpaceReservationService;

        public ParkingSpaceReservationsController(IParkingSpaceReservationService parkingSpaceReservationService)
        {
            _parkingSpaceReservationService = parkingSpaceReservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseParkingSpaceDto>>> Get()
        {
            return Ok(await _parkingSpaceReservationService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(BookingDto bookingDto)
        {
            var reservationId = await _parkingSpaceReservationService.Add(bookingDto);
            return CreatedAtAction(nameof(Post), new { id = reservationId });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _parkingSpaceReservationService.Delete(id);
            return NoContent();
        }
    }
}
