using Microsoft.EntityFrameworkCore;
using ParkingPlace.Modules.ParkingSpaces.Core.Data;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Repositories
{
    internal class ParkingSpaceRepository : IParkingSpaceRepository
    {
        private readonly ParkingSpaceDbContext _context;
        private readonly DbSet<ParkingSpace> _parkingSpace;

        public ParkingSpaceRepository(ParkingSpaceDbContext context, DbSet<ParkingSpace> parkingSpace)
        {
            _context = context;
            _parkingSpace = parkingSpace;
        }

        public async Task<IReadOnlyList<ParkingSpace>> GetAll()
        {
            return await _parkingSpace.ToListAsync();
        }

        public async Task<ParkingSpace?> Get(int parkingSpaceNumber)
        {
            return await _parkingSpace.SingleOrDefaultAsync(x => x.ParkingSpaceNumber == parkingSpaceNumber);
        }

        public async Task Add(ParkingSpace parkingSpace)
        {
            await _parkingSpace.AddAsync(parkingSpace);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ParkingSpace parkingSpace)
        {
            _context.Set<ParkingSpace>().Remove(parkingSpace);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ParkingSpace parkingSpace)
        {
            _parkingSpace.Update(parkingSpace);
            await _context.SaveChangesAsync();
        }
    }
}
