using Domian;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AreaRepository : BaseRepository<BA_SysArea>, IAreaRepository
    {
        public AreaRepository(IBaseClient client) : base(client)
        {
        }
    }
}
