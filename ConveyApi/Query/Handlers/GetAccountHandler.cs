using System.Threading.Tasks;
using Convey.CQRS.Queries;

namespace ConveyApi
{
    public class GetAccountHandler : IQueryHandler<GetAccount, AccountDto>
    {
        public Task<AccountDto> HandleAsync(GetAccount request)
        {
            return Task.FromResult(new AccountDto
            {
                Email = "Hello@world.com",
                Password = "Foo",
                AccountId = request.AccountId,
            });
        }
    }
}
