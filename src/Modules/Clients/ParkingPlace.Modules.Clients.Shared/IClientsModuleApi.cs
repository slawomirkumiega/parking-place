using ParkingPlace.Modules.Clients.Shared.DTO;

namespace ParkingPlace.Modules.Clients.Shared
{
    public interface IClientsModuleApi
    {
        Task<ResponseClientDto?> GetClient(Guid userId);
    }
}
