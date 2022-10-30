using Bank_System.Models;

namespace Bank_System.Interfaces
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();
        Account GetAccount(string username);
        bool CreateAccount(Account account, byte[] passwordHash, byte[] passwordSalt);
        bool UpdateAccount(Account account);
        bool AccountExist(string username);
        bool SaveChange();
    }
}
