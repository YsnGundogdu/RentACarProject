using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class UserForLoginDto : IDto
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
