using ParkingPlace.Modules.ParkingSpaces.Core.Entities;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Repositories
{
    internal interface IParkingSpaceReservationRepository
    {
        Task<IReadOnlyList<Reservation>> GetAll();
        Task<Reservation?> Get(Guid id);
        Task Add(Reservation reservation);
        Task Delete(Reservation reservation);
        Task Update(Reservation reservation);
    }
}
