using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebSocketManager;

namespace ChatServe
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuração da Injeção de dependência para o UserService
            services.AddWebSocketManager();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Configuração da Injeção de dependências
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

            //Ativa o uso de websockets e configura a rota /ws
            app.UseWebSockets();
            app.MapWebSocketManager("/ws", serviceProvider.GetService<ChatHandler>());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
