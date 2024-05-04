using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DataAccess.DataContracts;
using TimeTracker.DataAccess.Registration;
using TimeTracker.Specifications;
using TimeTracker.Specifications.Base;

namespace TimeTracker.DataAccess
{
    // TODO: Fix all namespaces, and make them file scoped
    // TODO: remove unnecessary usings
    // TODO: Make implementations internal
    public class DataAccessor : IDataAccessor
    {
        private readonly SQLiteAsyncConnection _db;

        public DataAccessor() 
        {
            _db = DatabaseConnectionFactory.GetConnection();
        }

        public async Task<T> Create<T>(T entity) where T : IEntity, new()
        {
            await _db.InsertAsync(entity);
            return entity;
        }

        public async Task<IList<T>> GetAllWhere<T>(ISpecification<T> specification) where T : IEntity, new()
        {
            return await _db.Table<T>().Where(specification.IsSatisfied).ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultWhere<T> (ISpecification<T> specification) where T : IEntity, new()
        {
            return await _db.Table<T>().FirstOrDefaultAsync(specification.IsSatisfied);
        }

        public async Task<T> GetFirstWhere<T>(ISpecification<T> specification) where T : IEntity, new()
        {
            return await _db.Table<T>().FirstAsync(specification.IsSatisfied);
        }

        public async Task<T?> GetById<T>(int id) where T: IEntity, new()
        {
            return await _db.Table<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update<T>(T entity) where T : IEntity, new()
        { 
            await _db.UpdateAsync(entity);
        }

        public async Task Delete<T>(int id) where T : IEntity, new()
        {
            await _db.DeleteAsync<T>(id);
        }
    }
}
