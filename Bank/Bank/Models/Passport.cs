using System;
using System.Collections.Generic;

namespace Bank_System.Models
{
    public partial class Passport
    {
        public string Username { get; set; } = null!;
        public byte[] Front { get; set; } = null!;
        public byte[] Back { get; set; } = null!;

        public virtual Account UsernameNavigation { get; set; } = null!;
    }
}
