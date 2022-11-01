using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Api
{
    public class SignalrHub : Hub<IMessageHubClient>
    {
        public async Task SendOffersToUser(CommentRequestDTO request)
        {
            await Clients.All.SendOffersToUser(request);
        }
    }
}