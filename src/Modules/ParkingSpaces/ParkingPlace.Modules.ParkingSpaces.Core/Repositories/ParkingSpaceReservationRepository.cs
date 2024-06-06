using Microsoft.EntityFrameworkCore;
using ParkingPlace.Modules.ParkingSpaces.Core.Data;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Repositories
{
    internal class ParkingSpaceReservationRepository : IParkingSpaceReservationRepository
    {
        private readonly ParkingSpaceDbContext _context;
        private readonly DbSet<Reservation> _reservation;

        public ParkingSpaceReservationRepository(ParkingSpaceDbContext context, DbSet<Reservation> reservation)
        {
            _context = context;
            _reservation = reservation;
        }

        public async Task<IReadOnlyList<Reservation>> GetAll()
        {
            return await _reservation.ToListAsync();
        }

        public async Task<Reservation?> Get(Guid id)
        {
            return await _reservation.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(Reservation reservation)
        {
            await _reservation.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Reservation reservation)
        {
            _context.Set<Reservation>().Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Reservation reservation)
        {
            _reservation.Update(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
