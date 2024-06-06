using Microsoft.AspNetCore.Mvc;
using ParkingPlace.Modules.ParkingSpaces.Core.Services;
using ParkingPlace.Modules.ParkingSpaces.Shared.DTO;

namespace ParkingPlace.Modules.ParkingSpaces.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingSpacesController : ControllerBase
    {
        private readonly IParkingSpaceService _parkingSpaceService;

        public ParkingSpacesController(IParkingSpaceService parkingSpaceService)
        {
            _parkingSpaceService = parkingSpaceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseParkingSpaceDto>>> Get()
        {
            return Ok(await _parkingSpaceService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CreateParkingSpaceDto parkingSpaceDto)
        {
            var parkingSpaceId = await _parkingSpaceService.Add(parkingSpaceDto);
            return CreatedAtAction(nameof(Post), new { id = parkingSpaceId });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int parkingSpaceNumber)
        {
            await _parkingSpaceService.Delete(parkingSpaceNumber);
            return NoContent();
        }
    }
}
