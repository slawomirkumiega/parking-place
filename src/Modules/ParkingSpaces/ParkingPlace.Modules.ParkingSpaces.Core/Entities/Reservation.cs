using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Entities
{
    internal sealed class Reservation
    {
        public Guid Id { get; private set; }
        public ParkingSpace ParkingSpace { get; private set; }
        public Guid ClientId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        //
        // Nie przekazuję tutaj referencji do ParkingSpace
        // Można to jeszcze tak zrobić, że zostanie przekazany ID lub Metoda Complete poniżej
        // (na potrzeby zadania...)
        //
        public Reservation(Guid id,
                           Guid clientId,
                           DateTime startDate,
                           DateTime endDate)
        {
            Id = id;
            ClientId = clientId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Reservation Complete(ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
            return this;
        }
    }
}
