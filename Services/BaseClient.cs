using Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaseClient : IBaseClient
    {
        readonly ISqlSugarClient _sugarClient;
        public BaseClient(ISqlSugarClient sqlSugarClient)
        {
            _sugarClient = sqlSugarClient;
        }

        /// <summary>
        /// 保持全局SqlSugarClient唯一性
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient GetBaseClient()
        {
            return _sugarClient as SqlSugarClient;
        }

        public void BeginTran()
        {
            GetBaseClient().BeginTran();
        }

        public void CommitTran()
        {
            GetBaseClient().CommitTran();
        }
        public void RollbackTran()
        {
            GetBaseClient().RollbackTran();
        }
    }
}
