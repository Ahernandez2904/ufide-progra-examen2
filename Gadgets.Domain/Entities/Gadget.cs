using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gadgets.Domain.Entities
{
    public class Gadget
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Marca { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }

        public string Tipo { get; set; }
    }
}
