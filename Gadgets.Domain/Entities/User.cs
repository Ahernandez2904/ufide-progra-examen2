using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Domain.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
