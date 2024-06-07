using static ParkingPlace.Shared.Repository.IRepository;

namespace ParkingPlace.Shared.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
