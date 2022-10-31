using Bank_System.Interfaces;
using Bank_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController : ControllerBase
    {
        private readonly IPassportRepository _passportRepository;
        private readonly IAccountRepository _accountRepository;

        public PassportController(Bank_SystemContext context, IPassportRepository passportRepository, IAccountRepository accountRepository)
        {
            _passportRepository = passportRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetPassports()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var passports = _passportRepository.GetPassports();
            return Ok(passports);
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetPassport(string username)
        {
            if (!_accountRepository.AccountExist(username)) return NotFound("Account doesn't exist");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var passports = _passportRepository.GetPassport(username);
            return Ok(passports);
        }

        [HttpPost("UploadImage/{username}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UploadImage(string username, List<IFormFile> fileUpload)
        {
            if (!_accountRepository.AccountExist(username)) return BadRequest("Account doesn't exist");
            if (fileUpload.Count < 0) return StatusCode(500, "There are problems during upload");

            int n = 0;
            byte[] front = new byte[255], back = new byte[255];

            foreach (var file in fileUpload)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    if (n == 0)
                    {
                        front = fileBytes;
                        n++;
                    }
                    else back = fileBytes;
                }
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            _passportRepository.CreatePassport(username, front, back);
            return Ok("Upload Done!");
        }
    }
}
