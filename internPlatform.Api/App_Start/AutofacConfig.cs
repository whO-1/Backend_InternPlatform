using Autofac;
using Autofac.Integration.WebApi;
using internPlatform.Application.Services;
using internPlatform.Application.Services.FilesOperations;
using internPlatform.Infrastructure.Data;
using internPlatform.Infrastructure.Repository;
using internPlatform.Infrastructure.Repository.IRepository;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Http;

namespace internPlatform.Api.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;
        public static void Register()
        {

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var builder = new ContainerBuilder();


            builder.Register(c => new ProjectDBContext(connectionString))
           .AsSelf()
           .InstancePerLifetimeScope();

            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<ApiService>().As<IApiService>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(Container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}