using Gadgets.Application.Models.InputModels;
using Gadgets.Application.Models.ViewModels;
using Gadgets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Contracts.Logics
{
    public interface IUserLogic
    {
        Task<string> GenerarToken(LoginInputModel user);

        bool ValidarToken(string token);

        Task<User> Buscar(string userName, string password);

        Task<ResponseViewModel> RegistrarUsuario(UserInputModel user);

        Task Cerrar();
    }
}
