using FinTracker.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CurrentUserService> _logger;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, ILogger<CurrentUserService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public string Id
        {
            get
            {
                try
                {
                    var user = _httpContextAccessor?.HttpContext?.User;

                    if (user == null || !user.Identity.IsAuthenticated)
                    {
                        _logger.LogWarning("User is not authenticated");
                        return null;
                    }

                    // Log all claims for debugging
                    foreach (var claim in user.Claims)
                    {
                        _logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
                    }

                    var nameIdentifierClaim = user.FindFirst(ClaimTypes.NameIdentifier)
                        ?? user.FindFirst("sub")
                        ?? user.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier", StringComparison.OrdinalIgnoreCase));

                    if (nameIdentifierClaim == null)
                    {
                        _logger.LogWarning("NameIdentifier claim not found");
                        return null;
                    }

                    if (Guid.TryParse(nameIdentifierClaim.Value, out Guid userId))
                    {
                        return userId.ToString();
                    }

                    _logger.LogWarning($"Failed to parse user ID as GUID: {nameIdentifierClaim.Value}");
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting user ID");
                    return null;
                }
            }
        }

        public string Email => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        public string FirstName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value;

        public string LastName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Surname)?.Value;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        Guid ICurrentUser.Id => (Id != string.Empty) || (Id != null) ? Guid.Parse(Id) : Guid.Empty;
    }
}
