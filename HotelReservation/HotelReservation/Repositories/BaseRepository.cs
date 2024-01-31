
using System.Linq.Expressions;
using HotelReservation.Data;
using HotelReservation.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HotelReservation.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseMetadataEntity //Where T : BaseEntityClass
    {

        protected readonly HotelReservationDbContext _dbContext;
        protected DbSet<T> _table;


        public BaseRepository(HotelReservationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this._table = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T?> GetById<TKey>(TKey entityId)
        {
            return await _table.FindAsync(entityId);
        }


        public async Task<T> Add(T entity)
        {
            await _table.AddAsync(entity);
            return entity;
        }


        public async Task<T?> Update<TKey>(TKey oldEntityId, T entity)
        {
            var oldEntity = await _table.FindAsync(oldEntityId);
            if (oldEntity != null && !oldEntity.IsDeleted)
            {
                entity.CreatedDate = oldEntity.CreatedDate;
                entity.CreatedBy = oldEntity.CreatedBy;
                _table.Update(entity);
                return (entity);
            }
            return null;
        }

        public async Task<T?> Delete<TKey>(TKey entityId)
        {
            var entity = await  _table.FindAsync(entityId);
            if (entity != null )
            {
                _table.Remove(entity);
            }
             
            return entity;
         
        }
        
        public async Task<T?> MetaDelete<TKey>(TKey entityId)
        {
            var entity = await _table.FindAsync(entityId);
            if (entity != null && entity.IsDeleted == false)
            {
                entity.IsDeleted = true;
                _table.Update(entity);
                return entity;
            }

            return null;
        }
       
    }
}
