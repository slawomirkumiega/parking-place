namespace ParkingPlace.Modules.ParkingSpaces.Core.Exceptions
{
    internal sealed class ReservationAlreadyExistsException : Exception
    {
        public Guid Id { get; }

        public ReservationAlreadyExistsException(Guid id)
            : base($"Reservation with id: '{id}' already exists.")
        {
            Id = id;
        }
    }
}
