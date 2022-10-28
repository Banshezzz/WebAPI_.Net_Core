using Bank_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly Bank_SystemContext _context;

        public BanksController(Bank_SystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Account), 200)]
        public IActionResult GetAll()
        {
            var getAll = _context.Banks.ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(getAll);
        }
    }
}
