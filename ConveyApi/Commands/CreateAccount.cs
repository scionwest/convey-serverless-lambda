using System;
using Convey.CQRS.Commands;

namespace ConveyApi
{
    public class CreateAccount : ICommand
    {
        public CreateAccount(Guid id, string email, string password)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Email = email;
            Password = password;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
