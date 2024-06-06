namespace ParkingPlace.Modules.ParkingSpaces.Shared.DTO
{
    public class ResponseReservationDto
    {
        public Guid Id { get; set; }
        public int ParkingSpaceNumber { get; set; }
        public Guid ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
