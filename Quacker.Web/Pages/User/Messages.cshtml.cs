using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quacker.Dal.Dto;
using Quacker.Dal.Services;

namespace Quacker.Web.Pages.User
{
    public class MessagesModel : PageModel
    {
        private readonly UserService userService;
        private readonly MessageService messageService;

        public MessagesModel( UserService userService, MessageService messageService)
        {
            this.userService = userService;
            this.messageService = messageService;
        }

        [BindProperty(SupportsGet = true, Name = "id")]
        public int UserId { get; set; }

        public List<UserHeader> ChatRooms { get; set; }

        public List<MessageData> Messages { get; set; }

        public string CurrentChat { get; set; }
        public int CurrentUserId { get; set; }

        public void OnGet()
        {
            CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ChatRooms = messageService.GetChatRooms(CurrentUserId);
            if (ChatRooms.Count > 0)
            {
                if (UserId == 0)
                {
                    CurrentChat = ChatRooms[0].DisplayName;
                    UserId = ChatRooms[0].Id;
                } else
                {
                    CurrentChat = userService.GetUser(CurrentUserId, UserId).DisplayName;
                }
                Messages = messageService.GetMessages(CurrentUserId, UserId);
            } else
            {
                if (UserId != 0)
                {
                    CurrentChat = userService.GetUser(CurrentUserId, UserId).DisplayName;
                }
            }
        }
    }
}
