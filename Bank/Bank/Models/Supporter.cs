using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Supporter
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int? Phone { get; set; }
        public string BankCode { get; set; } = null!;

        public virtual Bank BankCodeNavigation { get; set; } = null!;
    }
}
