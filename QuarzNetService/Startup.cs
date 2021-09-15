using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuarzNetService.Infrastructure;

namespace QuarzNetService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddQuartz(cfg =>
            {
                cfg.UseMicrosoftDependencyInjectionJobFactory();
                
                // Create a "key" for the job
                var jobKey = new JobKey("BackgroundJob");

                // Register the job with the DI container
                cfg.AddJob<BackgroundJob>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                // cfg.AddTrigger(opts => opts
                //     .ForJob(jobKey) // link to the HelloWorldJob
                //     .WithSchedule()
                    // .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
                    // .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully?
                options.WaitForJobsToComplete = true;
            });
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}