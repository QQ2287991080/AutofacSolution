using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacWebApi.Models
{
    public class EntypeModel
    {
        public decimal Id { get; set; }
        public Nullable<decimal> IdSysEnType { get; set; }
        public Nullable<decimal> IdParent { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public Nullable<bool> IsSys { get; set; }
        public Nullable<bool> IsEnd { get; set; }
        public Nullable<byte> Level { get; set; }
        public string RefSource { get; set; }
        public string Memo { get; set; }
        
    }
}
