using Microsoft.AspNetCore.SignalR;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Hubs
{
	public class myHub:Hub
	{
        private readonly ApplicationDbContext _context;
        public myHub(ApplicationDbContext context)
        {
            _context = context;
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            if (Context.User.IsInRole("Admin"))
            {
				await Groups.AddToGroupAsync(Context.ConnectionId,"Admin");
			}
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(string id,string message)
        {
            var senderId = Context.UserIdentifier;
            string receive = "";
            string sender = "";
            if (Context.User.IsInRole("Admin"))
            {
                sender = "Admin";
                receive = id;
            }
            else
            {
                sender = senderId;
                receive = "Admin";
            }
            var messages = new Message()
            {
                SenderId = sender,
                ReceiverId = receive,
                DateTime = DateTime.UtcNow,
                content = message
            };
            await _context.Messages.AddAsync(messages);
            await _context.SaveChangesAsync();
            await Clients.Group(id).SendAsync("ReceiveMessage", message);
		}
    }
}
