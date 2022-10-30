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

        public bool CreatePassport(string username, string[] images)
        {
            var passport = new Passport()
            {
                Username = username,
                Front = images[0],
                Back = images[1],
            };

            _context.Passports.Add(passport);
            return (_context.SaveChanges() > 0 ? true : false);
        }

        public string GetFilePath(string username)
        {
            return _environment.WebRootPath + "\\Uploads\\Accounts\\" + username;
        }

    }
}
