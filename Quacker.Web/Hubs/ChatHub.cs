using Ganss.XSS;
using Humanizer;
using Microsoft.AspNetCore.SignalR;
using Quacker.Dal.Dto;
using Quacker.Dal.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacker.Web.Hubs
{
    public class ChatHub: Hub
    {
        public UserService UserService { get; }
        public MessageService MessageService { get; }

        public ChatHub(UserService userService, MessageService messageService)
        {
            UserService = userService;
            MessageService = messageService;
        }


        public async Task SendMessage(int userId, string message)
        {
            // 1. Get current User
            var currentUserId = int.Parse(Context.UserIdentifier);
            var currentUser = await UserService.GetCurrentUserAsync(currentUserId);

            // 2. Save to DB
            var sanitizer = new HtmlSanitizer();
            message = sanitizer.Sanitize(message);
            await MessageService.CreateMessage(new NewMessage
            {
                Content = message,
                SenderId = currentUserId,
                ReceiverId = userId
            });

            // 3. Send back to current User
            await Clients.Caller.SendAsync("ReceiveMessage", currentUser, DateTime.UtcNow.Humanize(), message);

            // 4. Send to receiver
            if(OnlineUsers.TryGetValue(userId, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", currentUser, DateTime.UtcNow.Humanize(), message);
            }
        }

        private static ConcurrentDictionary<int,string> OnlineUsers { get; } = new ConcurrentDictionary<int,string>();

        public override async Task OnConnectedAsync()
        {
            var currentUserId = int.Parse(Context.UserIdentifier);
            var connectionId = Context.ConnectionId;
            // save to dictionary
            OnlineUsers.TryAdd(currentUserId, connectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var currentUserId = int.Parse(Context.UserIdentifier);
            // remove from dictonary
            OnlineUsers.Remove(currentUserId, out _);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
