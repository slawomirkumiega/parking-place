namespace ParkingPlace.Modules.ParkingSpaces.Core.Exceptions
{
    internal sealed class ReservationNotFoundException : Exception
    {
        public Guid Id { get; }

        public ReservationNotFoundException(Guid id) 
            : base($"Reservation with id: '{id}' doesn't exists.")
        {
            Id = id;
        }
    }
}
