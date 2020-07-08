using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBaseClient
    {
        /// <summary>
        /// 获取SqlSugarClient
        /// </summary>
        /// <returns></returns>
        SqlSugarClient GetBaseClient();
        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran();
        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTran();
    }
}
