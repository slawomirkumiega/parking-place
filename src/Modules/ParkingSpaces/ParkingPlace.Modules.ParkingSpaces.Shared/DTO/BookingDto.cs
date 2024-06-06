namespace ParkingPlace.Modules.ParkingSpaces.Shared.DTO
{
    public class BookingDto
    {
        public int ParkingSpaceNumber { get; set; }
        public Guid ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
