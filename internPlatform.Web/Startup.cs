using Hangfire;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Diagnostics;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(internPlatform.Web.Startup))]
namespace internPlatform.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseCors(CorsOptions.AllowAll);

            //Hangfire
            Hangfire.GlobalConfiguration.Configuration
                .UseSqlServerStorage(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            //Hangfire
            RecurringJob.AddOrUpdate(
                "LogToConsole",
                () => Debug.WriteLine("This message is logged every minute."),
                Cron.Minutely);
        }
    }
}
