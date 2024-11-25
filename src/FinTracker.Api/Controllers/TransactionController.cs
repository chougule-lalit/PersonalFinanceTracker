using FinTracker.Application.DTOs;
using FinTracker.Application.Services.Interfaces;
using FinTracker.Domain.Common.Interfaces;
using FinTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionAppService _transactionAppService;
        private readonly ICurrentUser _currentUserService;

        public TransactionController(
            ITransactionAppService transactionAppService,
            ICurrentUser currentUserService)
        {
            _transactionAppService = transactionAppService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionDto>>> GetUserTransactions()
        {
            var transactions = await _transactionAppService.GetUserTransactionsAsync(_currentUserService.Id);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(Guid id)
        {
            var transaction = await _transactionAppService.GetByIdAsync(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(CreateTransactionDto input)
        {
            var transaction = await _transactionAppService.CreateAsync(input);
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, UpdateTransactionDto input)
        {
            if (id != input.Id) return BadRequest();
            await _transactionAppService.UpdateAsync(input);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            await _transactionAppService.DeleteAsync(id);
            return NoContent();
        }
    }
}
