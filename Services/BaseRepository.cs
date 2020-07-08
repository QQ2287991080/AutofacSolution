using Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private SqlSugarClient _sugarClient;
        readonly IBaseClient _baseClient;
        private ISqlSugarClient _db
        {
            get
            {
                //if (typeof(TEntity).GetTypeInfo().GetCustomAttributes(typeof(SugarTable), true).FirstOrDefault((x => x.GetType() == typeof(SugarTable))) is SugarTable sugarTable && !string.IsNullOrEmpty(sugarTable.TableDescription))
                //{
                //    _sugarClient.ChangeDatabase(sugarTable.TableDescription.ToLower());
                //}
                //else
                //{
                //    _sugarClient.ChangeDatabase("1");
                //}
                return _sugarClient;
            }
        }
        protected internal ISqlSugarClient Db => _db;

        public BaseRepository(IBaseClient client)
        {
            _baseClient = client;
            _sugarClient = client.GetBaseClient();
        }
        #region Select
        public async Task<List<TEntity>> QueryAsync()
        {
            return await _db.Queryable<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression)
        {
           return await _db.Queryable<TEntity>().Where(expression).ToListAsync();
        }
        #endregion
        #region Add
        public async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }
        #endregion
        #region Update
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }
        public async Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> expression)
        {
            return await _db.Updateable(columns).Where(expression).ExecuteCommandHasChangeAsync();
        }
        public async Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, string updateWhere)
        {
            return await _db.Updateable(columns).Where(updateWhere).ExecuteCommandHasChangeAsync();
        }
        public async Task<bool> UpdateRangeAsync(List<TEntity> entities)
        {
            return await _db.Updateable(entities).ExecuteCommandHasChangeAsync();
        }
      
        #endregion
       
    }
}
