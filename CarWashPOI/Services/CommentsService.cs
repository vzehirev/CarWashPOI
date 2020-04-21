using CarWashPOI.Data;
using CarWashPOI.Data.Models;
using System;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext dbContext;

        public CommentsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddLocationCommentAsync(string userId, int locationId, string comment)
        {
            Comment commentToAdd = new Comment
            {
                UserId = userId,
                LocationId = locationId,
                Content = comment,
                AddedOn = DateTime.UtcNow,
            };

            dbContext.Comments.Add(commentToAdd);

            return await dbContext.SaveChangesAsync();
        }
    }
}
