using Microsoft.Extensions.Logging;
using ParkingPlace.Modules.ParkingSpaces.Core.Exceptions;
using ParkingPlace.Modules.ParkingSpaces.Shared.DTO;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;
using ParkingPlace.Modules.ParkingSpaces.Core.Repositories;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Services
{
    internal sealed class ParkingSpaceService : IParkingSpaceService
    {
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        private readonly ILogger<ParkingSpaceService> _logger;

        public ParkingSpaceService(ILogger<ParkingSpaceService> logger, IParkingSpaceRepository parkingSpaceRepository)
        {
            _logger = logger;
            _parkingSpaceRepository = parkingSpaceRepository;
        }

        public async Task<Guid> Add(CreateParkingSpaceDto parkingSpaceDto)
        {
            await ValidateParkingSpace(parkingSpaceDto.ParkingSpaceNumber);

            var parkingSpaceId = Guid.NewGuid();
            var parkingSpace = new ParkingSpace(parkingSpaceId, parkingSpaceDto.ParkingSpaceNumber, (Entities.PlaceStatus)parkingSpaceDto.Status);
            await _parkingSpaceRepository.Add(parkingSpace);

            _logger.LogInformation($"Parking Space with id: '{parkingSpaceId}' has been created successfully.");
            return parkingSpaceId;
        }

        public async Task Delete(int parkingSpaceNumber)
        {
            var parkingSpace = await _parkingSpaceRepository.Get(parkingSpaceNumber)
                ?? throw new ParkingSpaceNotFoundException(parkingSpaceNumber);

            await _parkingSpaceRepository.Delete(parkingSpace);
            _logger.LogInformation($"The parking space with parking space number: " +
                $"'{parkingSpace.ParkingSpaceNumber}' has been deleted.");
        }

        public async Task<IReadOnlyList<ResponseParkingSpaceDto>> GetAll()
        {
            var parkingSpaces = await _parkingSpaceRepository.GetAll();

            //
            // poniższe mapowanie MapToResponseParkingPlaceDto może zostać zastąpione autoMapperem
            //
            return parkingSpaces.Select(MapToResponseParkingPlaceDto).ToList();
        }

        public async Task<ResponseParkingSpaceDto?> Get(int parkingSpaceNumber)
        {
            var parkingSpace = await _parkingSpaceRepository.Get(parkingSpaceNumber);

            //
            // poniższe mapowanie MapToResponseParkingPlaceDto może zostać zastąpione autoMapperem
            //
            return parkingSpace is null ? null : MapToResponseParkingPlaceDto(parkingSpace);
        }

        private async Task ValidateParkingSpace(int parkingSpaceNumber)
        {
            var parkingSpaces = await _parkingSpaceRepository.Get(parkingSpaceNumber);
            if (parkingSpaces is not null)
            {
                throw new ParkingSpaceAlreadyExistsException(parkingSpaceNumber);
            }
        }

        private static ResponseParkingSpaceDto MapToResponseParkingPlaceDto(ParkingSpace parkingSpaces)
        {
            return new ResponseParkingSpaceDto
            {
                Id = parkingSpaces.Id,
                ParkingSpaceNumber = parkingSpaces.ParkingSpaceNumber,
                Status = (Shared.DTO.PlaceStatus)parkingSpaces.Status
            };
        }
    }
}
