using System.Threading.Tasks;

namespace CarWashPOI.Services.Comments
{
    public interface ICommentsService
    {
        Task<int> AddLocationCommentAsync(string userId, int locatinId, string comment);
    }
}
