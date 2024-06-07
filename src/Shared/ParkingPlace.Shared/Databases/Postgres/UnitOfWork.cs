using Microsoft.EntityFrameworkCore;
using ParkingPlace.Shared.Repository;
using static ParkingPlace.Shared.Repository.IRepository;

namespace ParkingPlace.Shared.Databases.Postgres
{
    internal class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        public TDbContext DbContext { get; }
        private bool disposed = false;
        private Dictionary<Type, object> repositories;

        public UnitOfWork(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<TEntity>(DbContext);
            }
            return (IRepository<TEntity>)repositories[type];
        }
    }
}
