namespace ParkingPlace.Modules.ParkingSpaces.Core.Entities
{
    internal sealed class ParkingSpace
    {
        public Guid Id { get; private set; }
        public int ParkingSpaceNumber { get; private set; }
        public PlaceStatus Status { get; private set; }

        public ParkingSpace(Guid id, int parkingSpaceNumber, PlaceStatus status)
        {
            Id = id;
            ParkingSpaceNumber = parkingSpaceNumber;
            Status = status;
        }

        // Tutaj mamy 2 stany. Można to zmienić na enuma w razie potrzeby i rozbudowy
        public void SetReservation(bool status = true)
        {
            Status = status ? PlaceStatus.Occupied : PlaceStatus.Free;
        }
    }
}
