using ParkingPlace.Modules.ParkingSpaces.Shared.DTO;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Services
{
    public interface IParkingSpaceService
    {
        Task<IReadOnlyList<ResponseParkingSpaceDto>> GetAll();
        Task<Guid> Add(CreateParkingSpaceDto clientDto);
        Task Delete(int parkingSpaceNumber);
    }
}
