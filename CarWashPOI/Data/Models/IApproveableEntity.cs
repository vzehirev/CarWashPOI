using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public interface IApproveableEntity
    {
        [Required]
        public bool IsApproved { get; set; }
    }
}
