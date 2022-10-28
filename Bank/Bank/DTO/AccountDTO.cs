namespace Bank_System.DTO
{
    public class AccountDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int BankCode { get; set; }
        public string Email { get; set; } = null!;
        public int? Phone { get; set; }
        public string PassportId { get; set; } = null!;
    }
}
