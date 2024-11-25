using FinTracker.Application.DTOs;
using FinTracker.Application.Services.Interfaces;
using FinTracker.Domain.Common.Interfaces;
using FinTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountAppService _bankAccountAppService;
        private readonly ICurrentUser _currentUserService;

        public BankAccountController(
            IBankAccountAppService bankAccountAppService,
            ICurrentUser currentUserService)
        {
            _bankAccountAppService = bankAccountAppService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BankAccountDto>>> GetUserAccounts()
        {
            var accounts = await _bankAccountAppService.GetUserAccountsAsync(_currentUserService.Id);
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccountDto>> GetAccount(Guid id)
        {
            var account = await _bankAccountAppService.GetByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<BankAccountDto>> CreateAccount(CreateBankAccountDto input)
        {
            var account = await _bankAccountAppService.CreateAsync(input);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, UpdateBankAccountDto input)
        {
            if (id != input.Id) return BadRequest();
            await _bankAccountAppService.UpdateAsync(input);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _bankAccountAppService.DeleteAsync(id);
            return NoContent();
        }
    }
}
