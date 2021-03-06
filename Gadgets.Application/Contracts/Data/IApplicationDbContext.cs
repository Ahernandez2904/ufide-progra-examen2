using Gadgets.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Contracts.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Gadget> Gadgets { get; set; }

        DbSet<Weather> Weather { get; }

        void Guardar();
    }
}
