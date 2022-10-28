using System;
using System.Collections.Generic;

namespace Bank_System.Models
{
    public partial class Supporter
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int? Phone { get; set; }
        public int BankCode { get; set; }

        public virtual Bank BankCodeNavigation { get; set; } = null!;
    }
}
