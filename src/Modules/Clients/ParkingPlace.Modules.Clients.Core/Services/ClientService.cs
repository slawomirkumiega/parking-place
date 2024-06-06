using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParkingPlace.Modules.Clients.Core.Data;
using ParkingPlace.Modules.Clients.Core.Entities;
using ParkingPlace.Modules.Clients.Core.Exceptions;
using ParkingPlace.Modules.Clients.Shared.DTO;

namespace ParkingPlace.Modules.Clients.Core.Services
{
    internal sealed class ClientService : IClientService
    {
        private readonly ClientsDbContext _db;
        private readonly ILogger<ClientService> _logger;

        public ClientService(ClientsDbContext db, ILogger<ClientService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Guid> Add(CreateClientDto clientDto)
        {
            await ValidateClient(clientDto);

            var clientId = Guid.NewGuid();
            var client = new Client(clientId, clientDto.FirstName, clientDto.Surname, clientDto.PhoneNumber);
            await _db.Clients.AddAsync(client);
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Client with id: '{client.Id}' has been created successfully.");
            return clientId;
        }

        public async Task Delete(Guid id)
        {
            var client = await _db.Clients.SingleOrDefaultAsync(x => x.Id == id) 
                ?? throw new ClientNotFoundException(id);

            _db.Set<Client>().Remove(client);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"The client with id: '{client.Id}' and phone number: '{client.PhoneNumber}' has been deleted.");
        }

        public async Task<IReadOnlyList<ResponseClientDto>> GetAll()
        {
            var clients = await _db.Clients.ToListAsync();

            //
            // poniższe mapowanie MapToResponseClientDto może zostać zastąpione autoMapperem
            //
            return clients.Select(MapToResponseClientDto).ToList();
        }

        public async Task<ResponseClientDto?> Get(Guid userId)
        {
            var client = await _db.Clients.Where(x => x.Id == userId).SingleOrDefaultAsync();

            if (client == null)
            {
                return null;
            }

            //
            // poniższe mapowanie MapToResponseClientDto również może zostać zastąpione autoMapperem
            //
            return MapToResponseClientDto(client);            
        }

        private async Task ValidateClient(CreateClientDto clientDto)
        {
            if (await _db.Clients.AnyAsync(x => x.PhoneNumber == clientDto.PhoneNumber))
            {
                throw new ClientAlreadyExistsException(clientDto.PhoneNumber);
            }
        }

        private static ResponseClientDto MapToResponseClientDto(Client client)
        {
            return new ResponseClientDto
            {
                Id = client.Id,
                FirstName = client.FirstName,
                Surname = client.Surname,
                PhoneNumber = client.PhoneNumber
            };
        }
    }
}
