using Bank_System.Models;

namespace Bank_System.Interfaces
{
    public interface IPassportRepository
    {
        ICollection<Passport> GetPassports();
        Passport GetPassport(string username);
        bool CreatePassport(string username, byte[] front, byte[] back);
        bool PassportExist(string username);

    }
}
