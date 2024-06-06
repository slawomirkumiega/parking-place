using ParkingPlace.Modules.Clients.Shared.DTO;

namespace ParkingPlace.Modules.Clients.Core.Services
{
    public interface IClientService
    {
        Task<IReadOnlyList<ResponseClientDto>> GetAll();
        Task<ResponseClientDto?> Get(Guid userId);
        Task<Guid> Add(CreateClientDto clientDto);
        Task Delete(Guid id);
    }
}
