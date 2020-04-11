using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tut_apdb_4.Middleware;
using tut_apdb_4.Services;

namespace tut_apdb_4
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
            //add studentNumber interface
            services.AddSingleton<IStudentNumberService, StudentNumberService>();
            services.AddSingleton<IStudentDbService, StudentDbService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStudentNumberService service)// add student number here
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<LoggingMiddleware>();
            app.Use(async (context,next) =>
            {
                if (!context.Request.Headers.ContainsKey("index"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("There was no index");
                    return;
                }
                var index = context.Request.Headers["Index"].ToString();
                if (!service.exist(index))
                    await context.Response.WriteAsync("student with provided index number doesn't exist");
                await next();
            });
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
