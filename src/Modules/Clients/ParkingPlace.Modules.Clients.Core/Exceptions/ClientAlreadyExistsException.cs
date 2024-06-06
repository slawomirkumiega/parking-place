namespace ParkingPlace.Modules.Clients.Core.Exceptions
{
    internal sealed class ClientAlreadyExistsException : Exception
    {
        public string PhoneNumber { get; }

        public ClientAlreadyExistsException(string phoneNumber) : base($"Client with phone number: '{phoneNumber}' already exists.")
        {
            PhoneNumber = phoneNumber;
        }
    }
}
