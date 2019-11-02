using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConveyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // If we are running outside of the Lambda environment, run as a standard netcoreapp.
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME")))
            {
                IHostBuilder hostBuilder = CreateHostBuilder(args);
                var built = hostBuilder.Build();
                built.Run();
            }
            else
            {
                // Wrap the hostbuilder inside of an AWS Lambda function handler.
                var lambdaEntry = new LambdaEntryPoint();
                var functionHandler = (Func<APIGatewayProxyRequest, ILambdaContext, Task<APIGatewayProxyResponse>>)(lambdaEntry.FunctionHandlerAsync);
                using (var handlerWrapper = HandlerWrapper.GetHandlerWrapper(functionHandler, new JsonSerializer()))
                using (var bootstrap = new LambdaBootstrap(handlerWrapper))
                {
                    bootstrap.RunAsync().Wait();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.ConfigureCustomApp());
    }
}
