using System.Linq.Expressions;

namespace HotelReservation.Repositories
{
    public interface IBaseRepository<T>
    {

        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById<TKey>(TKey entityId);
        Task<T> Add(T entity);

        //Guid??
        Task<T?> Delete<TKey>(TKey id);
        Task<T?> MetaDelete<TKey>(TKey id);
        Task<T?> Update<TKey>(TKey oldEntityId, T entity);

    }
}
