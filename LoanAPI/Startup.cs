using Castle.Facilities.AspNetCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DbContext = LoanAPI.Data.DBRepository.Concrete.StudentLoanDbContext;
using LoanAPI.Services;
using LoanAPI.Services.Repository.Abstract.ServiceAbstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using LoanAPI.Controllers;

namespace LoanAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static readonly WindsorContainer Container = new WindsorContainer();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoanAPI", Version = "v1" });
            });

            services.AddCors();

            // Custom application component registrations, ordering is important here
            RegisterApplicationComponents(services);

            // Castle Windsor integration, controllers, tag helpers and view components, this should always come after RegisterApplicationComponents
            services.AddWindsor(Container, opts => opts.UseEntryAssembly(typeof(LoanController).Assembly));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.AllowAnyOrigin()//("https://witty-beach-0aaa3850f.1.azurestaticapps.net")
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoanAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private void RegisterApplicationComponents(IServiceCollection services)
        {
            // ActivitySignupService components
            Container.Register(Component.For<ILoanService>().ImplementedBy<LoanService>()
                     .LifestyleScoped().IsDefault());
        }
    }
}
