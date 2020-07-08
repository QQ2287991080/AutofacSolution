using Domian;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EnTypeRepository : BaseRepository<BA_SysEnType>, IEntypeRepository
    {
        public EnTypeRepository(IBaseClient client) : base(client)
        {
        }
    }
}
