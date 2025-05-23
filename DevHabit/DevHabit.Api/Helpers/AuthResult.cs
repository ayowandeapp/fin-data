using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevHabit.Api.Helpers
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? UserId { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}