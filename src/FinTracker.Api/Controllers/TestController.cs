using FinTracker.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestAuthController : ControllerBase
    {
        private readonly ILogger<TestAuthController> _logger;
        private readonly ICurrentUser _currentUser;

        public TestAuthController(ILogger<TestAuthController> logger, ICurrentUser currentUser)
        {
            _logger = logger;
            _currentUser = currentUser;
        }

        [HttpGet("anonymous")]
        public IActionResult AnonymousEndpoint()
        {
            return Ok(new { Message = "This endpoint is accessible without authentication" });
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            // Log the auth header for debugging
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            _logger.LogInformation($"Auth header present: {!string.IsNullOrEmpty(authHeader)}");

            // Log authentication status
            _logger.LogInformation($"IsAuthenticated: {User.Identity?.IsAuthenticated}");

            // Log all claims
            _logger.LogInformation("User Claims:");
            foreach (var claim in User.Claims)
            {
                _logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
            }

            return Ok(new
            {
                Message = "This endpoint requires authentication",
                IsAuthenticated = User.Identity?.IsAuthenticated,
                CurrentUserId = _currentUser.Id,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}
