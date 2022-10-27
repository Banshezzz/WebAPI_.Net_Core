using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PassportId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
