using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Data.Models
{
    public class Town
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
