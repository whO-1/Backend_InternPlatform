using Autofac;
using Autofac.Integration.Mvc;
using internPlatform.Application.Services;
using internPlatform.Application.Services.FilesOperations;
using internPlatform.Application.Services.Mappings;
using internPlatform.Application.Services.Statistics;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
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
        public static IContainer Container { get; private set; }


        protected void Application_Start()
        {


            //DataBase
            Database.SetInitializer(new DbInitializer());

            //Areas,Filters,Routes
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            //autofac
            var builder = new ContainerBuilder();

            //DI 
            builder.Register(c => new ProjectDBContext(connectionString))
            .AsSelf()
            .InstancePerLifetimeScope();


            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<UserRoleService>().As<IUserRoleService>();

            builder.RegisterType<FaqConvertor>().As<IBaseConvertor<Faq, FaqDTO>>();
            builder.RegisterType<EntryTypeConvertor>().As<IBaseConvertor<EntryType, EntryTypeDTO>>();
            builder.RegisterType<AgeGroupConvertor>().As<IBaseConvertor<AgeGroup, AgeGroupDTO>>();
            builder.RegisterType<LinkConvertor>().As<IBaseConvertor<Link, LinkDTO>>();
            builder.RegisterType<CategoryConvertor>().As<IBaseConvertor<Category, CategoryDTO>>();
            builder.RegisterType<ImageConvertor>().As<IBaseConvertor<Image, ImageDTO>>();

            builder.RegisterGeneric(typeof(EntityManageService<,>)).As(typeof(IEntityManageService<,>));
            builder.RegisterType<LinkEntityManageService>().As<ILinkEntityManageService>();
            builder.RegisterType<ImageEntityService>().As<IImageEntityService>();
            builder.RegisterType<ErrorsService>().As<IErrorsService>();
            builder.RegisterType<EventManageService>().As<IEventManageService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<StatisticsService>().As<IStatisticsService>();

            //Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.Register(ctx => HttpContext.Current.GetOwinContext()).As<IOwinContext>();

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

        }
    }
}
