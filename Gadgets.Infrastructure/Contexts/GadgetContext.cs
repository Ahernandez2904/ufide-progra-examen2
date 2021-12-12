using Gadgets.Application.Contracts.Contexts;
using Gadgets.Application.Contracts.Data;
using Gadgets.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Infrastructure.Contexts
{
    public class GadgetContext : IGadgetContext
    {
        public GadgetContext(IApplicationDbContext dbContext)
        {
            Database = dbContext;
        }

        readonly IApplicationDbContext Database;

        public Gadget Buscar(string nombre)
        {
            string sql = "EXEC Gadgets_Get @nombre";
            List<SqlParameter> parameters =
                new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@nombre", Value = nombre}
                };

            var lista =
                Database.Gadgets.FromSqlRaw<Gadget>
                    (sql, parameters.ToArray()).ToList();
            if (lista.Count > 0)
            {
                return lista.FirstOrDefault();
            }

            return null;
        }
    }
}
