namespace Base.Exchange.OrderGenerator.App.Components.Services
{
    using Microsoft.AspNetCore.SignalR;
    public class FixHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
