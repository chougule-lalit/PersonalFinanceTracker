using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain.Entities.Auth
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ApplicationRole() : base()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }

}
