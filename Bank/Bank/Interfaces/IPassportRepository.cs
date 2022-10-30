using Bank_System.Models;

namespace Bank_System.Interfaces
{
    public interface IPassportRepository
    {
        bool CreatePassport(string username, string[] images);
        string GetFilePath(string username);

    }
}
