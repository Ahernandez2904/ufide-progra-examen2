using Gadgets.Application.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Models.ViewModels
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; }

        public LoginInputModel Input { get; set; }
    }
}
