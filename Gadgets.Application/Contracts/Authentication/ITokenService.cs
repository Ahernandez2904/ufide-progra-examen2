using Gadgets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Contracts.Authentication
{
    public interface ITokenService
    {
        string Authenticate(User user);

        bool Validate(string token);
    }
}
