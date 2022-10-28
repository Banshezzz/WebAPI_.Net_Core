﻿using AutoMapper;
using Bank_System.DTO;
using Bank_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly Bank_SystemContext _context;
        private readonly IMapper _mapper;

        public AccountsController(Bank_SystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
        public IActionResult GetAccounts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_mapper.Map<List<AccountDTO>>(_context.Accounts.ToList()));
        }

        [HttpGet("{AccountName}")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public IActionResult GetAccount(string AccountName)
        {
            var account = _mapper.Map<AccountDTO>(_context.Accounts.Where(a => a.Username == AccountName).FirstOrDefault());
            if (account == null) return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(account);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] AccountDTO account)
        {
            if (account == null) return BadRequest(ModelState);

            var createAccount = new Account()
            {
                Username = account.Username,
                Password = account.Password,
                PassportId = account.PassportId,
                BankCode = account.BankCode,
                Email = account.Email,
                Phone = account.Phone,
                Status = 0,
            };

            _context.Accounts.Add(createAccount);
            return Ok(_context.SaveChanges() > 0 ? true : false);
        }
    }
}
