using AutoMapper;
using Evolution.Buiness.Interface;
using Evolution.Buiness.Repository;
using Evolution.Data.Context;
using Evolution.Data.Interface;
using Evolution.Data.Repository;
using Evolution.Infraestructure.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Evolution.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //services.AddAutoMapper(typeof(Startup));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EvolutionProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddDbContext<EvolutionContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DevDB")), ServiceLifetime.Transient);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Evolution.Api", Version = "v1" });
            });
            LoadScopes(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Evolution.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region private method
        private static void LoadScopes(IServiceCollection services)
        {
            services.AddScoped(typeof(IDefaultRepository<>), typeof(DefaultRepository<>));
            services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
            services.AddScoped<IProductoBusiness, ProductoBusiness>();
            services.AddScoped<IPedidoBusiness, PedidoBusiness>();
        }
        #endregion
    }
}
