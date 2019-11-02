using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Convey.CQRS.Queries;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConveyApi
{
    public class GetAccountHandler : IQueryHandler<GetAccount, AccountDto>
    {
        private readonly IAmazonDynamoDB dynamoDbClient;
        private readonly string accountsTableName;

        public GetAccountHandler(IAmazonDynamoDB dynamoDbClient, IConfiguration configuration)
        {
            this.dynamoDbClient = dynamoDbClient;
            this.accountsTableName = configuration["AWS:DynamoDb:AccountsTable:TableName"];
        }

        public async Task<AccountDto> HandleAsync(GetAccount request)
        {
            Table accountsTable = Table.LoadTable(this.dynamoDbClient, this.accountsTableName);
            var getOperation = new GetItemOperationConfig
            {
                AttributesToGet = new List<string> { "Id", "Email", "Password" },
                ConsistentRead = true
            };
            Document accountDocument = await accountsTable.GetItemAsync(request.AccountId, getOperation);
            var account = new AccountDto
            {
                Email = accountDocument[nameof(AccountDto.Email)],
                Id = Guid.Parse(accountDocument[nameof(AccountDto.Id)]),
                Password = accountDocument[nameof(AccountDto.Password)],
            };

            return account;
        }
    }
}
