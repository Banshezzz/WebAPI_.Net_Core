using Bank_System.Interfaces;
using Bank_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_System.Repository
{
    public class PassportRepository : IPassportRepository
    {
        private readonly Bank_SystemContext _context;
        private readonly IWebHostEnvironment _environment;

        public PassportRepository(Bank_SystemContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public bool CreatePassport(string username, byte[] front, byte[] back)
        {
            var passport = new Passport()
            {
                Username = username,
                Front = front,
                Back = back,
            };

            _context.Passports.Add(passport);
            return (SaveChange());
        }

        public Passport GetPassport(string username)
        {
            return _context.Passports.Where(x => x.Username == username).FirstOrDefault();
        }

        public ICollection<Passport> GetPassports()
        {
            return (_context.Passports.ToList());
        }

        public bool PassportExist(string username)
        {
            return (_context.Passports.Any(p => p.Username == username));
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() > 0 ? true : false);
        }
    }
}
