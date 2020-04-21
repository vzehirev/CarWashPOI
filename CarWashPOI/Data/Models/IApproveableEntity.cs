using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Data.Models
{
    public interface IApproveableEntity
    {
        [Required]
        public bool IsApproved { get; set; }
    }
}
