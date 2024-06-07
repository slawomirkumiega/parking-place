
using System.Linq.Expressions;

namespace ParkingPlace.Shared.Repository
{
    public interface IRepository
    {
        public interface IRepository<TEntity> where TEntity : class
        {
            Task<IReadOnlyList<TEntity>> GetAll();
            Task<TEntity> Get(Expression<Func<TEntity, bool>> expression);
            Task Add(TEntity entity);
            void Delete(TEntity entity);
            void Update(TEntity entity);
        }
    }
}
