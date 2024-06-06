using System.ComponentModel.DataAnnotations;

namespace ParkingPlace.Modules.ParkingSpaces.Shared.DTO
{
    public class ParkingSpaceDto
    {
        [Required]
        public required int ParkingSpaceNumber { get; set; }

        [Required]
        public required PlaceStatus Status { get; set; }
    }
}
