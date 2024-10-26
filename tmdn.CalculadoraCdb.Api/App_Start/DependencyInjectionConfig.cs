using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using tmdn.CalculadoraCdb.Adapters.CdbDatabase.Repositories;
using tmdn.CalculadoraCdb.Application.Ports.Cdb;
using tmdn.CalculadoraCdb.Application.Services;

namespace tmdn.CalculadoraCdb.Api
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterDependencyInjection()
        {
            var builder = new ContainerBuilder();

            RegisterApplication(builder);
            RegisterCdbDatabase(builder);

            _ = builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterApplication(ContainerBuilder container)
        {
            _ = container.RegisterType<CdbService>().AsSelf();
        }

        private static void RegisterCdbDatabase(ContainerBuilder builder)
        {
            _ = builder.RegisterType<CdbRepository>().As<ICdbPort>();
        }
    }
}