using AutoMapper;
using CarWashPOI.Data;
using CarWashPOI.Data.Models;
using CarWashPOI.ViewModels.Comments;
using System;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CommentsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> AddLocationCommentAsync(CommentInputModel inputModel)
        {
            Comment commentToAdd = mapper.Map<Comment>(inputModel);
            commentToAdd.AddedOn = DateTime.UtcNow;
            dbContext.Comments.Add(commentToAdd);

            return await dbContext.SaveChangesAsync();
        }
    }
}
