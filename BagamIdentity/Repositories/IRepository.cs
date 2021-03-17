using BagamIdentity.Entities;
using System;
using System.Linq;

namespace BagamIdentity.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(Guid id);
        IQueryable<T> GetAll();
        T Add(T entity);
    }
}
