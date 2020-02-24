using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacWebApi.App_Start
{
    public class AutofacModule:Autofac.Module
    {
        public bool ObeySpeedLimit { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            
        }
    }
}