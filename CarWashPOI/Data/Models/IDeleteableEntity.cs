using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public interface IDeleteableEntity
    {
        [Required]
        public bool IsDeleted { get; set; }
    }
}
