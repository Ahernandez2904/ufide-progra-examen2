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
    public class WeatherContext : IWeatherContext
    {
        public WeatherContext(IApplicationDbContext dbContext)
        {
            Database = dbContext;
        }

        readonly IApplicationDbContext Database;
    }
}
