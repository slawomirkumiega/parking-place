using ParkingPlace.Modules.ParkingSpaces.Core.Entities;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Repositories
{
    internal interface IParkingSpaceRepository
    {
        Task<IReadOnlyList<ParkingSpace>> GetAll();
        Task<ParkingSpace?> Get(int parkingSpaceNumber);
        Task Add(ParkingSpace parkingSpace);
        Task Delete(ParkingSpace parkingSpace);
        Task Update(ParkingSpace parkingSpace);
    }
}
