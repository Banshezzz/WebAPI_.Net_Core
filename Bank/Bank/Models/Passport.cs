using System;
using System.Collections.Generic;

namespace Bank_System.Models
{
    public partial class Passport
    {
        public string Username { get; set; } = null!;
        public string Front { get; set; } = null!;
        public string Back { get; set; } = null!;

        public virtual Account UsernameNavigation { get; set; } = null!;
    }
}
