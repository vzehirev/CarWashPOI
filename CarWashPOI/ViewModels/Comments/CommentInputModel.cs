using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.ViewModels.Comments
{
    public class CommentInputModel
    {
        [Required]
        public int LocationId { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
