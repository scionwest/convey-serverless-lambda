using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ConveyApi
{
    public static class IWebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureCustomApp(this IWebHostBuilder webBuilder)
        {
            webBuilder.ConfigureAppConfiguration((hostBuilder, configBuilder) =>
            {
                configBuilder.AddJsonFile("appsettings.json");
                if (!hostBuilder.HostingEnvironment.EnvironmentName.Equals("Production", System.StringComparison.OrdinalIgnoreCase))
                {
                    configBuilder.AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment.EnvironmentName}.json");
                }
            });

            webBuilder.ConfigureServices(services =>
            {
                services.AddConvey()
                    .AddAWS()
                    .AddInMemoryCommandDispatcher()
                    .AddInMemoryQueryDispatcher()
                    .AddCommandHandlers()
                    .AddQueryHandlers()
                    .AddWebApi()
                    .Build();
            });

            webBuilder.Configure(app =>
            {
                app.UseInitializers()
                    .UseDispatcherEndpoints(endpoints =>
                    {
                        endpoints.Get("", context => context.Response.WriteJsonAsync(new { Message = "Hello World" }))
                            .Get("ping", context => context.Response.WriteJsonAsync(new { Message = "pong" }))
                            .Get<GetAccount, AccountDto>(path: "accounts/{accountId}");
                    });
            });
            return webBuilder;
        }
    }
}
