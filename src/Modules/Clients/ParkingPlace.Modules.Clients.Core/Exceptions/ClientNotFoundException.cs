namespace ParkingPlace.Modules.Clients.Core.Exceptions
{
    internal sealed class ClientNotFoundException : Exception
    {
        public Guid Id { get; }

        public ClientNotFoundException(Guid id) : base($"Client with id: '{id}' doesn't exists.")
        {
            Id = id;
        }
    }
}
