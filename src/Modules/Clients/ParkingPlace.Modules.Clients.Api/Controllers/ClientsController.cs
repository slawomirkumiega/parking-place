using Microsoft.AspNetCore.Mvc;
using ParkingPlace.Modules.Clients.Core.Services;
using ParkingPlace.Modules.Clients.Shared.DTO;

namespace ParkingPlace.Modules.Clients.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseClientDto>>> Get()
        {
            return Ok(await _clientService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CreateClientDto client)
        {
            var clientId = await _clientService.Add(client);
            return CreatedAtAction(nameof(Get), new { id = clientId });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _clientService.Delete(id);
            return NoContent();
        }
    }
}
