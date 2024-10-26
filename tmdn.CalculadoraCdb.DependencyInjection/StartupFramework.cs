using Autofac;
using Autofac.Integration.WebApi;
using tmdn.CalculadoraCdb.Adapters.CdbDatabase.Repositories;
using tmdn.CalculadoraCdb.Application.Ports.Cdb;

namespace tmdn.CalculadoraCdb.DependencyInjection
{
    public static class StartupFramework
    {
        private static AutofacWebApiDependencyResolver resolver;
        public static AutofacWebApiDependencyResolver GetResolver()
        {
            if (resolver != null)
                return resolver;

            var builder = new ContainerBuilder();

            CdbDatabase(builder);

            var container = builder.Build();

            resolver = new AutofacWebApiDependencyResolver(container);

            return resolver;
        }

        private static void CdbDatabase(ContainerBuilder builder)
        {
            _ = builder.RegisterType<CdbRepository>().As<ICdbPort>();
        }
    }
}
