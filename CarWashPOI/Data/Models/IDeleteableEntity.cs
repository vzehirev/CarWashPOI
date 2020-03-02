using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Data.Models
{
    public interface IDeleteableEntity
    {
        public bool IsDeleted { get; set; }
    }
}
