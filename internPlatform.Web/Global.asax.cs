using Autofac;
using Autofac.Integration.Mvc;
using internPlatform.App_Start;
using internPlatform.Application.Services;
using internPlatform.Infrastructure.Data;
using internPlatform.Infrastructure.Repository;
using internPlatform.Infrastructure.Repository.IRepository;
using Microsoft.Owin;
using System.Data.Entity;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace internPlatform.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //NLog
            NLogConfig Logger = new NLogConfig();
            Logger.Initialize();
            //DataBase
            Database.SetInitializer(new DbInitializer());

            //Areas,Filters,Routes
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            builder.Register(c => new ProjectDBContext(connectionString))
            .AsSelf()
            .InstancePerLifetimeScope();

            //DI 
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<UserRoleService>().As<IUserRoleService>();
            builder.RegisterGeneric(typeof(EntityManageService<>)).As(typeof(IEntityManageService<>));
            builder.RegisterType<LinkEntityManageService>().As<ILinkEntityManageService>();
            builder.RegisterType<EventManageService>().As<IEventManageService>();
            builder.RegisterType<ApiService>().As<IApiService>();

            //Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.Register(ctx => HttpContext.Current.GetOwinContext()).As<IOwinContext>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));




        }
    }
}
