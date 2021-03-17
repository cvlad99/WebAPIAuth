using System;

namespace BagamIdentity.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}
