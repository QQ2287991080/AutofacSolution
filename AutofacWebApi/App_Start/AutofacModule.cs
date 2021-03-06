﻿using Autofac;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.Extensions.EnumMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AutofacWebApi.App_Start
{
    public class AutofacModule : Autofac.Module
    {
        private readonly IEnumerable<Assembly> assembliesToScan;

        public AutofacModule(IEnumerable<Assembly> assembliesToScan) => this.assembliesToScan = assembliesToScan;

        public AutofacModule(params Assembly[] assembliesToScan) : this((IEnumerable<Assembly>)assembliesToScan) { }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var assembliesToScan = this.assembliesToScan as Assembly[] ?? this.assembliesToScan.ToArray();
            var allTypes = assembliesToScan
                          .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                          .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                          .SelectMany(a => a.DefinedTypes)
                          .Where(w=>w.IsAssignableFrom(typeof(IProfile)))//默认继承IProfile,排除不需要configuration的实例
                          .ToArray();

            var openTypes = new[] {
                            typeof(IValueResolver<,,>),
                            typeof(IMemberValueResolver<,,,>),
                            typeof(ITypeConverter<,>),
                            typeof(IValueConverter<,>),
                            typeof(IMappingAction<,>)
            };

            foreach (var type in openTypes.SelectMany(openType =>
                 allTypes.Where(t => t.IsClass && !t.IsAbstract && ImplementsGenericInterface(t.AsType(), openType))))
            {
                builder.RegisterType(type.AsType()).InstancePerDependency();
            }


            //configuration配置
            builder.Register<IConfigurationProvider>(ctx =>
            new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assembliesToScan);
                cfg.AllowNullCollections = true;//允许空集合
                cfg.DisableConstructorMapping();//禁止构造函数映射
                cfg.ShouldUseConstructor = ci => !ci.IsPrivate;//不映射私有构造函数
                cfg.AddExpressionMapping();//添加 表达式目录映射 AutoMapper.Extensions.ExpressionMapping
                /*cfg.EnableEnumMappingValidation();*///https://docs.automapper.org/en/latest/Enum-Mapping.html
            })
            );

            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();
        }

        private static bool ImplementsGenericInterface(Type type, Type interfaceType)
                  => IsGenericType(type, interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => IsGenericType(@interface, interfaceType));

        private static bool IsGenericType(Type type, Type genericType)
                   => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}