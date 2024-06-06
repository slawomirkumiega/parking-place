using System.ComponentModel.DataAnnotations;

namespace ParkingPlace.Modules.Clients.Shared.DTO
{
    public class ClientDto
    {
        [Required]
        [StringLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public required string Surname { get; set; }

        [Required]
        [StringLength(15)]
        public required string PhoneNumber { get; set; }
    }
}
