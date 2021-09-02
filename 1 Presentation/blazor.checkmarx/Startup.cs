using application.checkmarx;
using application.checkmarx.Commands;
using application.checkmarx.Commands.AddOrder;
using application.checkmarx.Queries;
using blazor.checkmarx.Data;
using blazor.checkmarx.Shared;
using crosscutting.checkmarx;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using persistence.checkmarx;
using services.checkmarxs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazor.checkmarx
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<IRabbitMQService, RabbitMQService>();
            services.AddSignalR();
            services.AddSingleton<IApplicationContext, ApplicationContext>();
            services.AddSingleton<ICommandHandler<AddOrderCommand>, AddOrderCommandHandler>();
            services.AddSingleton<ICommandHandler<ChangeOrderStatusCommand>, ChangeOrderStatusCommandHandler>();
            services.AddSingleton<IQueryHandler<GetDishesQuery>, GetDishesQueryHandler>();
            services.AddSingleton<IQueryHandler<GetOrderQueueQuery>, GetOrderQueueQueryHandler>();
            services.AddSingleton<IQueryHandler<GetChefsQuery>, GetChefsQueryHandler>();
            services.AddSingleton<IQueryHandler<GetWaitersQuery>, GetWaitersQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapHub<QueueHub>("/orderHub");
            });
            lifetime.ApplicationStarted.Register(() => RegisterSignalRWithRabbitMQ(app.ApplicationServices));
        }
        public void RegisterSignalRWithRabbitMQ(IServiceProvider serviceProvider)
        {
            // Connect to RabbitMQ
            var rabbitMQService = (IRabbitMQService)serviceProvider.GetService(typeof(IRabbitMQService));
            rabbitMQService.Connect(AppConstants.ORDER_QUEUE, "orderReceived");
            rabbitMQService.Connect(AppConstants.DELEIVERY_QUEUE,"deliveryReceived");
        }

    }
}
