using System;
using Convey.CQRS.Queries;

namespace ConveyApi
{
    public class GetAccount : IQuery<AccountDto>
    {
        public Guid AccountId { get; set; }
    }
}
