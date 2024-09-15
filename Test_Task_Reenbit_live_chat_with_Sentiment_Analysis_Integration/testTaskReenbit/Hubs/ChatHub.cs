using Microsoft.AspNetCore.SignalR;
using testTaskReenbit.Data;
using testTaskReenbit.Data.Entities;
using testTaskReenbit.TextAnalyticsAPI;

namespace testTaskReenbit.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;
        private readonly TextAnalyticsService _textAnalyticsService;

        public ChatHub(AppDbContext context, TextAnalyticsService textAnalyticsService)
        {
            _context = context;
            _textAnalyticsService = textAnalyticsService;
        }

        public async Task BroadcastMessage(string name, string message)
        {
            var sentiment = _textAnalyticsService.AnalyzeSentiment(message);
            var messageToArchive = new Message
            {
                UserName = name,
                Text = message,
                Timestamp = DateTime.UtcNow,
                Sentiment = sentiment
            };

            await _context.Messages.AddAsync(messageToArchive);
            await _context.SaveChangesAsync();
            await Clients.All.SendAsync("broadcastMessage", name, message, sentiment);
        }

        public Task Echo(string name, string message) =>
            Clients.Client(Context.ConnectionId)
                    .SendAsync("echo", name, $"{message} (echo from server)");
    }
}
