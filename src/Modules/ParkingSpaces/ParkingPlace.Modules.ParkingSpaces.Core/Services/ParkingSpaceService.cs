using Microsoft.Extensions.Logging;
using ParkingPlace.Modules.ParkingSpaces.Shared.DTO;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;
using static ParkingPlace.Shared.Repository.IRepository;
using ParkingPlace.Modules.ParkingSpaces.Core.Data;
using ParkingPlace.Shared.Databases.Postgres;
using ParkingPlace.Modules.ParkingSpaces.Core.Exceptions;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Services
{
    internal sealed class ParkingSpaceService : IParkingSpaceService
    {
        private readonly IRepository<ParkingSpace> _parkingSpaceRepository;
        private readonly ILogger<ParkingSpaceService> _logger;
        private readonly IUnitOfWork<ParkingSpaceDbContext> _unitOfWork;

        public ParkingSpaceService(ILogger<ParkingSpaceService> logger, IUnitOfWork<ParkingSpaceDbContext> unitOfWork)
        {
            _logger = logger;
            _parkingSpaceRepository = unitOfWork.GetRepository<ParkingSpace>();
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Add(CreateParkingSpaceDto parkingSpaceDto)
        {
            await ValidateParkingSpace(parkingSpaceDto.ParkingSpaceNumber);

            var parkingSpaceId = Guid.NewGuid();
            var parkingSpace = new ParkingSpace(parkingSpaceId, parkingSpaceDto.ParkingSpaceNumber, (Entities.PlaceStatus)parkingSpaceDto.Status);
            await _parkingSpaceRepository.Add(parkingSpace);
            _unitOfWork.Commit();

            _logger.LogInformation($"Parking Space with id: '{parkingSpaceId}' has been created successfully.");
            return parkingSpaceId;
        }

        public async Task Delete(int parkingSpaceNumber)
        {
            var parkingSpace = await _parkingSpaceRepository.Get(x => x.ParkingSpaceNumber == parkingSpaceNumber)
                ?? throw new ParkingSpaceNotFoundException(parkingSpaceNumber);

            _parkingSpaceRepository.Delete(parkingSpace);
            _unitOfWork?.Commit();

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
            var parkingSpace = await _parkingSpaceRepository.Get(x => x.ParkingSpaceNumber == parkingSpaceNumber);

            //
            // poniższe mapowanie MapToResponseParkingPlaceDto może zostać zastąpione autoMapperem
            //
            return parkingSpace is null ? null : MapToResponseParkingPlaceDto(parkingSpace);
        }

        private async Task ValidateParkingSpace(int parkingSpaceNumber)
        {
            var parkingSpaces = await _parkingSpaceRepository.Get(x => x.ParkingSpaceNumber == parkingSpaceNumber);
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
