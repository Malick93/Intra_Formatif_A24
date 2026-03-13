using Microsoft.AspNetCore.SignalR;
using SignalR.Events;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _pizzaManager.AddUser();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnConnectedAsync();

            _pizzaManager.RemoveUser();
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _pizzaManager.GetGroupName(choice));
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _pizzaManager.GetGroupName(choice));
        }

        public async Task AddMoney(PizzaChoice choice)
        {
            PizzaEvent pizza = _pizzaManager.IncreaseMoney(choice);

            await Clients.Group(_pizzaManager.GetGroupName(choice)).SendAsync("Event", pizza as PizzaEvent);
        }

        public async Task BuyPizza(PizzaChoice choice)
        {
           _pizzaManager.BuyPizza(choice);
        }
    }
}
