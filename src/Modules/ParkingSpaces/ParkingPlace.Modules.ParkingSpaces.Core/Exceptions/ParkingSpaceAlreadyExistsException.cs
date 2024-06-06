namespace ParkingPlace.Modules.ParkingSpaces.Core.Exceptions
{
    internal sealed class ParkingSpaceAlreadyExistsException : Exception
    {
        public int ParkingSpaceNumber { get; }

        public ParkingSpaceAlreadyExistsException(int parkingSpaceNumber)
            : base($"Parking Space with parking space number: '{parkingSpaceNumber}' already exists.")
        {
            ParkingSpaceNumber = parkingSpaceNumber;
        }
    }
}
