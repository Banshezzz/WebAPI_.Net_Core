using Bank_System.Models;

namespace Bank_System.Interfaces
{
    public interface IAuthRepository
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(Account account);

    }
}
