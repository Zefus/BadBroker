using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BadBroker.BusinessLogic;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.Services;
using BadBroker.DataAccess;
using BadBroker.WebService.Validation;

namespace BadBroker.WebService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BadBrokerContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddOptions();

            services.Configure<Config>(options => { options.AccessKey = Configuration.GetSection("AccessKey").Value; }) ;

            services
                .AddScoped<IModelValidator, ModelValidator>()
                .AddScoped<IStringToDateParser, StringToDateParser>()
                .AddScoped<IEnumerateDaysBetweenDates, EnumerateDaysBetweenDates>()
                .AddScoped<IDBService, DBRatesService>()
                .AddScoped<IExternalServiceClient, ExchangeRatesApiClient>()
                .AddScoped<IBestCaseSearcher, BestCaseSearcher>()
                .AddScoped<ITradeService, TradeService>()
                .AddScoped<IGetCachedRatesOperation, GetCachedRatesOperation>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BadBrokerContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/InternalError");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
