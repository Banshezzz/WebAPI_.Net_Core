using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Status { get; set; }
        public string BankCode { get; set; } = null!;

        public virtual Bank BankCodeNavigation { get; set; } = null!;
        public virtual Customer UsernameNavigation { get; set; } = null!;
    }
}
