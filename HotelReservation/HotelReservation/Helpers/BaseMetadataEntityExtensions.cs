using HotelReservation.Models.Domain;

namespace HotelReservation.Helpers
{
    public static class BaseMetadataEntityExtensions
    {
        public static T CreatedMetadata<T>(this T entity, string createBy) where T : BaseMetadataEntity
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = createBy;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = createBy;
            return entity;
        }
        public static T UpdatedMetadata<T>(this T entity , string updatedBy) where T : BaseMetadataEntity
        {
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = updatedBy;
            return entity;
        }

        public static T DeletedMetadata<T>(this T entity , string deletedBy) where T : BaseMetadataEntity
        {
            entity.DeletedDate = DateTime.Now;
            entity.DeletedBy = deletedBy;
            return entity;
        }
    }
}
