using System;
using System.Collections.Generic;

namespace Bank_System.Models
{
    public partial class Account
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Status { get; set; }
        public int BankCode { get; set; }
        public string Email { get; set; } = null!;
        public int? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string PassportId { get; set; } = null!;

        public virtual Bank BankCodeNavigation { get; set; } = null!;
    }
}
