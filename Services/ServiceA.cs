using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceA : IServiceA
    {
        public void Get()
        {
            Console.WriteLine("A");
        }

        public string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}
