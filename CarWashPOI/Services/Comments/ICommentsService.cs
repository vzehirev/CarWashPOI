using CarWashPOI.ViewModels.Comments;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Comments
{
    public interface ICommentsService
    {
        Task<int> AddLocationCommentAsync(CommentInputModel inputModel);
    }
}
