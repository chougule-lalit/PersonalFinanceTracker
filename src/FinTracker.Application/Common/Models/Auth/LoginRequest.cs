﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Common.Models.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
