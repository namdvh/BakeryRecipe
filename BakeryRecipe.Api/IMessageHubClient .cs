using BakeryRecipe.ViewModels.Users;

namespace BakeryRecipe.Api
{
    public interface IMessageHubClient
    {
        Task SendOffersToUser(CommentRequestDTO request);
    }
}
