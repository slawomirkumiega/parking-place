using Microsoft.Extensions.Logging;
using ParkingPlace.Modules.ParkingSpaces.Core.Exceptions;
using ParkingPlace.Modules.ParkingSpaces.Shared.DTO;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;
using ParkingPlace.Modules.Clients.Shared;
using ParkingPlace.Modules.ParkingSpaces.Core.Repositories;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Services
{
    internal sealed class ParkingSpaceReservationService : IParkingSpaceReservationService
    {
        private readonly ILogger<ParkingSpaceService> _logger;
        private readonly IClientsModuleApi _clientsModuleApi;
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        private readonly IParkingSpaceReservationRepository _parkingSpaceReservationRepository;

        public ParkingSpaceReservationService(
            ILogger<ParkingSpaceService> logger,
            IClientsModuleApi clientsModuleApi,
            IParkingSpaceRepository parkingSpaceRepository,
            IParkingSpaceReservationRepository parkingSpaceReservationRepository)
        {
            _logger = logger;
            _clientsModuleApi = clientsModuleApi;
            _parkingSpaceRepository = parkingSpaceRepository;
            _parkingSpaceReservationRepository = parkingSpaceReservationRepository;
        }

        public async Task<Guid> Add(BookingDto booking)
        {
            var client = await _clientsModuleApi.GetClient(booking.ClientId);
            var parkingSpace = await _parkingSpaceRepository.Get(booking.ParkingSpaceNumber);

            if (client == null)
            {
                throw new Exception("Client doesn't exists.");
            }            

            if (parkingSpace == null)
            {
                throw new Exception("Parking space doesn't exists.");
            }
            
            await ValidateReservation(parkingSpace.Id);

            var reservationId = Guid.NewGuid();
            var reservation = new Reservation(reservationId, client.Id, booking.StartDate, booking.EndDate)
                .Complete(parkingSpace);
            await _parkingSpaceReservationRepository.Add(reservation);

            // zrobiona jest podstawowa wersja rezerwacji
            //
            // TODO
            // aktualizacja statusu miejsca postojowego (await _parkingSpaceRepository.Update(parkingSpace));
            // dodać unit of work pattern
            // dodanie tranzakcji

            _logger.LogInformation($"Reservation with id: '{reservationId}' has been created successfully.");
            return reservationId;
        }

        public async Task Delete(Guid id)
        {
            var reservation = await _parkingSpaceReservationRepository.Get(id)
                ?? throw new ReservationNotFoundException(id);

            await _parkingSpaceReservationRepository.Delete(reservation);

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
            var reservation = await _parkingSpaceReservationRepository.Get(id);
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
                ParkingSpaceNumber = reservation.ParkingSpace.ParkingSpaceNumber,
                ClientId = reservation.ClientId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate
            };
        }
    }
}
