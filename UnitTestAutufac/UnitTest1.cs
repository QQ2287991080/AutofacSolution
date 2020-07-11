using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Web.Http.Results;
using Autofac;
using AutofacWebApi.App_Start;
using AutofacWebApi.Controllers;
using AutofacWebApi.Models;
using AutoMapper;
using Domian;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using SqlSugar;

namespace UnitTestAutufac
{
    [TestClass]
    public class UnitTest1
    {
        readonly Autofac.IContainer _container;
        public UnitTest1()
        {
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

            //automapper
            //module 注入   自动注入，将AutofacWebApi模块中所有的类都configuration，但是还是需要注册有对应的映射关系
            //builder.RegisterModule(new AutofacModule(Assembly.Load("AutofacWebApi")));

            builder.Register(r => new AutoMapperRegist().Register()).As<IMapper>().SingleInstance();
            // 获取http配置
            // var configuration = GlobalConfiguration.Configuration;
            //注册api控制器
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //注册Autofac筛选器提供程序。
            //builder.Register(r => new AutofacActionFilter()).AsWebApiActionFilterForAllControllers();//将过滤器放到容器中
            //builder.RegisterWebApiFilterProvider(configuration);
            //注册Autofac模型绑定器提供程序。
            //builder.RegisterWebApiModelBinderProvider();
            //将依赖关系解析程序设置为Autofac。
            _container = builder.Build();
            //configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        [TestMethod]
        public void TestMethod1()
        {
            var entype = _container.Resolve<IEntypeRepository>();
            var mapper = _container.Resolve<IMapper>();

            //实例化依赖注入mock对象
            var controller = new MapperController(mapper, entype);
            var result = (controller.Get()).Result;
            var json = result as JsonResult<List<EntypeModel>>;
            foreach (var item in json.Content)
            {
                Console.WriteLine(item.Name);
            }
            Assert.IsNotNull(result);
            
        }
    }
}
