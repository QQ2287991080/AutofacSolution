using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian
{
    public class MyContext
    {
        public SqlSugarClient GetInstance()
        {
            var _sugarClient = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source =.; Initial Catalog = KingkoilWarrantyCard; Integrated Security = True; User Id = sa; Pwd = 123456; ",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            //Print sql
            _sugarClient.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + _sugarClient.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
            return _sugarClient;
        }
    }
}
