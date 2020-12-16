using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api.AppMetrics.NET472.Infrastructure
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddControllersAsServices(this IServiceCollection services, IEnumerable<Type> controllerTypes)
        {
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);//˲̬ע�ᣬ�����ȡ-��GC����-�����ͷţ� ÿһ�λ�ȡ�Ķ��󶼲���ͬһ��������
            }

            return services;
        }

        public static IServiceCollection AddControllersAsServices(this IServiceCollection services)
        {
            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(ApiController).IsAssignableFrom(t)
                            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

            return services;
        }

        public static IServiceProvider SetDependencyResolver(this IServiceCollection services, HttpConfiguration httpConfiguration)
        {
            var provider = services.BuildServiceProvider();
            var resolver = new DefaultDependencyResolver(provider);
            httpConfiguration.DependencyResolver = resolver;
            return provider;
        }
    }
}