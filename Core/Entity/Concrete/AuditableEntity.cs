using Core.Entity.Abstract;
using System;

namespace Core.Entity.Concrete
{
    public class AuditableEntity : BaseEntity, ICreatedEntity, IUpdatedEntity
    {
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
