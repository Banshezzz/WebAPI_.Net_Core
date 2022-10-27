using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Bank
    {
        public Bank()
        {
            Accounts = new HashSet<Account>();
            Supporters = new HashSet<Supporter>();
        }

        public int Id { get; set; }
        public string? BankName { get; set; }
        public string BankCode { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Supporter> Supporters { get; set; }
    }
}
