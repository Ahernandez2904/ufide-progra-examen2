using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gadgets.Domain.Entities
{
    public class Weather
    {
//        [Key]
        [Required]
        public string Query { get; set; }

//        [Key]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RequestTime { get; set; }

        [Required]
        public string Result { get; set; }
    }
}
