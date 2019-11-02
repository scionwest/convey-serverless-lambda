using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace ConveyApi
{
    public class AccountService
    {
        private readonly ICommandDispatcher dispatcher;

        public AccountService(ICommandDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public Task CreateAccountAsync(CreateAccount command)
            => this.dispatcher.SendAsync(command);
    }
}
