using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceC : IServiceC
    {
        public void Get()
        {
            Console.WriteLine("C");
        }
    }
}
