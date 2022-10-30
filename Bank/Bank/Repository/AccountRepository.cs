using Bank_System.Interfaces;
using Bank_System.Models;

namespace Bank_System.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Bank_SystemContext _context;

        public AccountRepository(Bank_SystemContext context)
        {
            _context = context;
        }

        public bool AccountExist(string username)
        {
            return _context.Accounts.Any(x => x.Username == username);
        }

        public bool CreateAccount(Account account, byte[] passwordHash, byte[] passwordSalt)
        {
            var createAccount = new Account()
            {
                Username = account.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                BankCode = account.BankCode,
                Email = account.Email,
                Phone = account.Phone,
                PassportId = account.PassportId,
            };
            _context.Add(createAccount);
            return SaveChange();
        }

        public Account GetAccount(string username)
        {
            return _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
        }

        public ICollection<Account> GetAccounts()
        {
            return (_context.Accounts.ToList());
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateAccount(Account account)
        {
            var updateAccount = _context.Accounts.Where(a => a.Username == account.Username).FirstOrDefault();
            
            updateAccount.Phone = account.Phone;
            updateAccount.Birthday = account.Birthday;
            updateAccount.Address = account.Address;
            updateAccount.Email = account.Email;
            updateAccount.Status = account.Status;
            updateAccount.PassportId = account.PassportId;
            updateAccount.BankCode = account.BankCode;
            
            _context.Update(updateAccount);
            return SaveChange();
        }
    }
}
