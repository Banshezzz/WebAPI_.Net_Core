using AutoMapper;
using Bank_System.DTO;
using Bank_System.Models;
using Microsoft.AspNetCore.Mvc;
using Bank_System.Interfaces;

namespace Bank_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AccountsController(IMapper mapper, IAccountRepository accountRepository, IAuthRepository authRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _authRepository = authRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
        public IActionResult GetAccounts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accounts = _mapper.Map<List<AccountDTO>>(_accountRepository.GetAccounts());
            return Ok(accounts);
        }

        [HttpGet("{accountName}")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public IActionResult GetAccount(string accountName)
        {
            var account = _mapper.Map<AccountDTO>(_accountRepository.GetAccount(accountName));
            if (account == null) return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(account);
        }

        [HttpPost("Register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] RegisterDTO request)
        {
            if (request == null) return BadRequest(ModelState);
            if (_accountRepository.AccountExist(request.Username))
            {
                ModelState.AddModelError("", "Account existed");
                return StatusCode(422, ModelState);
            }
            _authRepository.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var account = _mapper.Map<Account>(request);

            if (!_accountRepository.CreateAccount(account, passwordHash, passwordSalt))
            {
                ModelState.AddModelError("", "Something went wrong when creating!");
                return StatusCode(500, ModelState);
            }

            return Ok(account);
        }

        [HttpPost("Login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Login(LoginDTO request)
        {
            var account = _accountRepository.GetAccount(request.Username);

            if (account == null) return NotFound();

            if (!_accountRepository.AccountExist(account.Username))
                return NotFound();

            if (!_authRepository.VerifyPasswordHash(request.Password, account.PasswordHash, account.PasswordSalt))
                return BadRequest("Wrong password!");

            string token = _authRepository.CreateToken(account);
            return Ok(token);
        }

        [HttpPut("{accountName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult updateAccount(string accountName, [FromBody] AccountDTO request)
        {
            if (accountName == null) return BadRequest();
            if (!_accountRepository.AccountExist(request.Username)) return NotFound();

            var updateAccount = _mapper.Map<Account>(request);

            if (!_accountRepository.UpdateAccount(updateAccount))
            {
                ModelState.AddModelError("", "Something went wrong updating account");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
