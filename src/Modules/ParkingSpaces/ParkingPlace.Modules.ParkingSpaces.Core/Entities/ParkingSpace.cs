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
    }
}
