namespace ParkingPlace.Modules.ParkingSpaces.Core.Exceptions
{
    internal sealed class ParkingSpaceNotFoundException : Exception
    {
        public int ParkingSpaceNumber { get; }

        public ParkingSpaceNotFoundException(int parkingSpaceNumber) 
            : base($"Parking Space with number: '{parkingSpaceNumber}' doesn't exists.")
        {
            ParkingSpaceNumber = parkingSpaceNumber;
        }
    }
}
