﻿using ETicaret.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;

namespace SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task OrderAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage, message);

        }
    }
}
