using FinTracker.Application.DTOs;
using FinTracker.Application.Services.Interfaces;
using FinTracker.Domain.Common.Interfaces;
using FinTracker.Domain.Enums;
using FinTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionCategoryController : ControllerBase
    {
        private readonly ITransactionCategoryAppService _categoryAppService;
        private readonly ICurrentUser _currentUserService;

        public TransactionCategoryController(
            ITransactionCategoryAppService categoryAppService,
            ICurrentUser currentUserService)
        {
            _categoryAppService = categoryAppService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionCategoryDto>>> GetUserCategories()
        {
            var categories = await _categoryAppService.GetUserCategoriesAsync(_currentUserService.Id);
            return Ok(categories);
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<List<TransactionCategoryDto>>> GetCategoriesByType(TransactionType type)
        {
            var categories = await _categoryAppService.GetCategoriesByTypeAsync(type);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionCategoryDto>> GetCategory(Guid id)
        {
            var category = await _categoryAppService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionCategoryDto>> CreateCategory(CreateTransactionCategoryDto input)
        {
            var category = await _categoryAppService.CreateAsync(input);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateTransactionCategoryDto input)
        {
            if (id != input.Id) return BadRequest();
            await _categoryAppService.UpdateAsync(input);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryAppService.DeleteAsync(id);
            return NoContent();
        }
    }
}
