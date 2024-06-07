using Microsoft.EntityFrameworkCore;
using ParkingPlace.Shared.Repository;

namespace ParkingPlace.Shared.Databases.Postgres
{
    public interface IUnitOfWork : IRepositoryFactory
    {
        int Commit();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        TContext DbContext { get; }
    }
}
