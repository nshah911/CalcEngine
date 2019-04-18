using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CalcEngine.API.App_Code;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;

namespace CalcEngine.API
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings." + env.EnvironmentName + ".json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _hostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            services.AddMvc();

            services.AddScoped<ILoanManager, IUSLoanManager>();
            services.AddScoped<IGetContentRequest, FileManagerApiClient>();
            services.AddSingleton<IConfiguration>(Configuration);

            builder.Register(d =>
            {
                return new LoggerFactory().CreateLogger("IUSCalcEngine");
            }).As<Microsoft.Extensions.Logging.ILogger>();

            builder.Populate(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
