namespace ParkingPlace.Modules.Clients.Core.Entities
{
    internal sealed class Client
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string PhoneNumber { get; private set; }

        public Client(Guid id, string firstName, string surname, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }
    }
}
