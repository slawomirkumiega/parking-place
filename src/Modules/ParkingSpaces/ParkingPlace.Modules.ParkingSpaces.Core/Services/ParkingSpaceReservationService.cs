using Microsoft.Extensions.Logging;
using ParkingPlace.Modules.ParkingSpaces.Core.Exceptions;
using ParkingPlace.Modules.ParkingSpaces.Shared.DTO;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;
using ParkingPlace.Modules.Clients.Shared;
using static ParkingPlace.Shared.Repository.IRepository;
using ParkingPlace.Modules.ParkingSpaces.Core.Data;
using ParkingPlace.Shared.Databases.Postgres;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Services
{
    internal sealed class ParkingSpaceReservationService : IParkingSpaceReservationService
    {
        private readonly ILogger<ParkingSpaceService> _logger;
        private readonly IClientsModuleApi _clientsModuleApi;
        private readonly IRepository<ParkingSpace> _parkingSpaceRepository;
        private readonly IRepository<Reservation> _parkingSpaceReservationRepository;
        private readonly IUnitOfWork<ParkingSpaceDbContext> _unitOfWork;

        public ParkingSpaceReservationService(
            ILogger<ParkingSpaceService> logger,
            IClientsModuleApi clientsModuleApi,
            IUnitOfWork<ParkingSpaceDbContext> unitOfWork)
        {
            _logger = logger;
            _clientsModuleApi = clientsModuleApi;
            _parkingSpaceRepository = unitOfWork.GetRepository<ParkingSpace>();
            _parkingSpaceReservationRepository = unitOfWork.GetRepository<Reservation>();
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Add(BookingDto booking)
        {           
            var client = await _clientsModuleApi.GetClient(booking.ClientId);
            var parkingSpace = await _parkingSpaceRepository.Get(
                a => a.ParkingSpaceNumber == booking.ParkingSpaceNumber);

            if (client == null)
            {
                throw new Exception("Client doesn't exists.");
            }

            if (parkingSpace == null)
            {
                throw new Exception("Parking space doesn't exists.");
            }

            await ValidateReservation(parkingSpace.Id);

            parkingSpace.SetReservation();

            var reservationId = Guid.NewGuid();
            var reservation = new Reservation(reservationId, client.Id, booking.StartDate, booking.EndDate)
                .Complete(parkingSpace);

            await _parkingSpaceReservationRepository.Add(reservation);
            _parkingSpaceRepository.Update(parkingSpace);
            _unitOfWork.Commit();

            _logger.LogInformation($"Reservation with id: '{reservationId}' has been created successfully.");
            return reservationId;
        }

        public async Task Delete(Guid id)
        {
            var reservation = await _parkingSpaceReservationRepository.Get(x => x.Id == id)
                ?? throw new ReservationNotFoundException(id);

            _parkingSpaceReservationRepository.Delete(reservation);
            _unitOfWork.Commit();

            _logger.LogInformation($"The reservation with id: '{id}' has been deleted.");
        }

        public async Task<IReadOnlyList<ResponseReservationDto>> GetAll()
        {
            var reservations = await _parkingSpaceReservationRepository.GetAll();

            //
            // poniższe mapowanie MapToResponseReservationDto może zostać zastąpione autoMapperem
            //
            return reservations.Select(MapToResponseReservationDto).ToList();
        }

        private async Task ValidateReservation(Guid id)
        {
            var reservation = await _parkingSpaceReservationRepository.Get(x => x.Id == id);
            if (reservation is not null)
            {
                throw new ReservationAlreadyExistsException(id);
            }
        }

        private static ResponseReservationDto MapToResponseReservationDto(Reservation reservation)
        {
            return new ResponseReservationDto
            {
                Id = reservation.Id,
                ClientId = reservation.ClientId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate
            };
        }
    }
}
