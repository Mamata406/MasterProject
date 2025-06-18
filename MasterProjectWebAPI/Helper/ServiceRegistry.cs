using MasterProjectBAL.Departments;
using MasterProjectBAL.Jobs;
using MasterProjectBAL.Locations;
using MasterProjectCommonUtility.Configuration;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Paging;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.DataModel.DepartmentsRepo;
using MasterProjectDAL.DataModel.JobsRepo;
using MasterProjectDAL.DataModel.LocationsRepo;
using MasterProjectDAL.EmailRepo;
using Microsoft.EntityFrameworkCore;

namespace MasterProjectWebAPI.Helper
{
    public class ServiceRegistry
    {
        public void ConfigureDependencies(IServiceCollection services, AppsettingsConfig appSettings)
        {
            #region Bussiness Layer
            // services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IJobsService, JobsService>();
            services.AddScoped<IDepartmentService,DepartementsService>();
            services.AddScoped<ILocationService,LocationService>();

           
            #endregion

            #region Data Layer
            //services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IJobsRepository, JobsRepository>();
            services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
            services.AddScoped<ILocationsRepository, LocationsRepository>();
           
            #endregion

            #region Common Layer
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<IPagingParameter, PagingParameter>();
            #endregion
        }
        public void ConfigureDataContext(IServiceCollection services, AppsettingsConfig appSettings)
        {
            //Added LogFactory
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            });
            var connString = appSettings.MasterProjectData.ConnectToDb.ConnectionString;
            services.AddDbContext<IMasterProjectContext, MasterProjectContext>(options =>
            {
                options.UseMySql(connString, new MySqlServerVersion(new Version(8, 0, 37))).EnableSensitiveDataLogging().UseLoggerFactory(loggerFactory);
            });
        }
    }
}
