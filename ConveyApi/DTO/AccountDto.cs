using System;

namespace ConveyApi
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
