using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {

        #region Select
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync();
        /// <summary>
        /// 获取满足条件的数据
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression);
       
        #endregion

        #region Add
        /// <summary>
        /// 根据实体新增
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        Task<int> AddAsync(TEntity entity);
        #endregion

        #region Update
        /// <summary>
        /// 用实体对象更新数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity);
        /// <summary>
        /// 根据条件更新指定实体字段
        /// </summary>
        /// <param name="columns">需要更新的字段</param>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Expression<Func<TEntity,TEntity>> columns,Expression<Func<TEntity,bool>> expression);
        /// <summary>
        /// 根据条件更新指定实体字段(SQL)
        /// </summary>
        /// <param name="columns">需要更新的字段</param>
        /// <param name="updateWhere">sql语句</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, string updateWhere);
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> UpdateRangeAsync(List<TEntity> entities);
        #endregion

        
    }
}
