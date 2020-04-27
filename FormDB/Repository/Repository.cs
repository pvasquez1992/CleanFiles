using FormDB.Dto;
using FormDB.Repository.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FormDB.Repository
{
    public class SqlRepository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public SqlRepository()
        {
            _dbContext = new BuildDbContext("myConnectionSql");

            if (_dbContext == null)
                throw new ArgumentNullException(nameof(_dbContext));

            _dbSet = _dbContext.Set<T>();


        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            try
            {

                if (filter == null)
                    throw new ArgumentNullException(nameof(filter));

                IQueryable<T> query = _dbSet.Where(filter);

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
