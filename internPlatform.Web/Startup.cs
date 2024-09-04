using internPlatform.App_Start;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

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
            HangfireConfig.ConfigureHangfire(app);

            //NLogger
            var loggerConfig = new NLogConfig();
            loggerConfig.Initialize();

        }

    }
}
