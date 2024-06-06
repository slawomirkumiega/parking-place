using ParkingPlace.Modules.ParkingSpaces.Shared.DTO;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Services
{
    public interface IParkingSpaceReservationService
    {
        Task<IReadOnlyList<ResponseReservationDto>> GetAll();
        Task<Guid> Add(BookingDto booking);
        Task Delete(Guid id);
    }
}
