using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian
{
    [SqlSugar.SugarTable("BA_SysArea")]
    public class BA_SysArea
    {
        [SqlSugar.SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public decimal Id { get; set; }
        public decimal? idSysArea { get; set; }
        public string Name { get; set; }
    }
}
