using ParkingPlace.Modules.Clients.Shared;
using ParkingPlace.Modules.Clients.Shared.DTO;

namespace ParkingPlace.Modules.Clients.Core.Services
{
    internal sealed class ClientsModuleApi : IClientsModuleApi
    {
        private readonly IClientService _clientService;

        public ClientsModuleApi(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<ResponseClientDto?> GetClient(Guid userId)
        {
            return await _clientService.Get(userId);
        }
    }
}
