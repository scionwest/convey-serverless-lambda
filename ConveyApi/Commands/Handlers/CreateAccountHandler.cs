using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace ConveyApi
{
    public class CreateAccountHandler : ICommandHandler<CreateAccount>
    {
        public async Task HandleAsync(CreateAccount command)
        {

        }
    }
}
