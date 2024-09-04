using Hangfire;
using Hangfire.SqlServer;
using internPlatform.Application.Services;
using internPlatform.Application.Services.FilesOperations;
using Owin;
using System;
using System.Web.Configuration;
using System.Web.Mvc;

namespace internPlatform.App_Start
{
    public class HangfireConfig
    {

        public static void ConfigureHangfire(IAppBuilder app)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });

            app.UseHangfireServer();
            app.UseHangfireDashboard();
            AddOrUpdateJobs();
        }

        public static void ConfigureHangfire()
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private static void AddOrUpdateJobs()
        {
            var bufferJobService = DependencyResolver.Current.GetService<IFileService>();
            var imageJobService = DependencyResolver.Current.GetService<IImageEntityService>();

            RecurringJob.AddOrUpdate("Buffer-Clear",
                () => bufferJobService.EmptyBuffer(),
            Cron.Hourly);

            RecurringJob.AddOrUpdate("ImageDB-Clear",
                () => imageJobService.RemoveTempImages(),
            Cron.Hourly);


        }
    }



}