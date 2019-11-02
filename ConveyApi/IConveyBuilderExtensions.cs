using Amazon;
using Amazon.DynamoDBv2;
using Convey;
using Microsoft.Extensions.DependencyInjection;

namespace ConveyApi
{
    public static class IConveyBuilderExtensions
    {
        public static IConveyBuilder AddAWS(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
            return builder;
        }
    }
}
