using System;

namespace Core.Entity.Abstract
{
    public interface IUpdatedEntity
    {
        int? UpdatedUserId { get; set; }
        DateTime? UpdatedTime { get; set; }
    }
}