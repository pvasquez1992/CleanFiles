using FormDB.Dto;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FormDB.Repository.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity, new()
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
    }
}
