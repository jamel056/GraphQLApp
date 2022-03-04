using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQLApp.Data;
using GraphQLApp.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQLApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("GraphQlConnectionStr"));
            });

            services.AddScoped<AppDbContext>();
            services.AddScoped<PlatformSchema>();
            services.AddScoped<CommandSchema>();

            services.AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(PlatformSchema), ServiceLifetime.Scoped)
                .AddGraphTypes(typeof(CommandSchema), ServiceLifetime.Scoped)
                .AddDataLoader();

            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseGraphQL<PlatformSchema>(path: "/platform");
            app.UseGraphQL<CommandSchema>(path: "/command");
            app.UseGraphQLPlayground(options: new PlaygroundOptions(), path:"/ui/playground");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
