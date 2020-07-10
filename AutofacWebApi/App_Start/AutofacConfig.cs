using Autofac;
using Autofac.Builder;
using Autofac.Integration.WebApi;
using AutofacWebApi.Filters;
using AutoMapper;
using Domian;
using Interfaces;
using Services;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace AutofacWebApi.App_Start
{
    public class AutofacConfig
    {
        /// <summary>
        /// 此方法用于程序初始化所用，在程序启动的时候容器开始工作，依赖倒转，webapi的控制器，过滤器都能够注册
        /// 控制器是必须要注册的，否则会报错，注册类型的时候采用的是读取config配置，下一次用的时候更加方便，其实这个看个人习惯。
        /// autofac和unity比起我个人觉得autofac更加舒服。
        /// </summary>
        public static void Register()
        {
            //初始化容器
            var builder = new ContainerBuilder();
            //注册服务
            var baseType = typeof(ISuperService);
            var interfaces = ConfigurationManager.AppSettings["IServices"];
            var services = ConfigurationManager.AppSettings["Services"];//从配置文件中加载
            var assembly = Assembly.Load(services);//加载类库中所有类
            /* builder.RegisterAssemblyTypes(assembly).Where(p=>baseType.IsAssignableFrom(p)).AsImplementedInterfaces();*///service必须以Service结尾，必须继承自ISuperService &&p.Name.EndsWith("Service")
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            var instance = new MyContext().GetInstance();

            builder.RegisterType<BaseClient>().As<IBaseClient>().InstancePerLifetimeScope();
            //builder.RegisterInstance<ISqlSugarClient>(instance).SingleInstance();

            builder.Register(c => new MyContext().GetInstance()).As<ISqlSugarClient>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();


            //builder.RegisterModule(new AutofacModule(Assembly.Load("AutofacWebApi")));
            //automapper
            builder.Register(r => new AutoMapperRegist().Register()).As<IMapper>().SingleInstance();
            //builder.RegisterType<ISqlSugarClient>().InstancePerLifetimeScope();
            //builder.RegisterSource<>
            //获取http配置
            var configuration = GlobalConfiguration.Configuration;
            //注册api控制器
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //注册Autofac筛选器提供程序。
            //builder.Register(r => new AutofacActionFilter()).AsWebApiActionFilterForAllControllers();//将过滤器放到容器中
            builder.RegisterWebApiFilterProvider(configuration);
            //注册Autofac模型绑定器提供程序。
            builder.RegisterWebApiModelBinderProvider();
            //将依赖关系解析程序设置为Autofac。
            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

    }
}