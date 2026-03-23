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
            var evt = _pizzaManager.AddUser();
            await Clients.All.SendAsync("UpdateNbUsers", evt.NbUsers);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var evt = _pizzaManager.RemoveUser();
            await Clients.All.SendAsync("UpdateNbUsers", evt.NbUsers);
            await base.OnDisconnectedAsync(exception);

        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _pizzaManager.GetGroupName(choice));
            await Clients.Caller.SendAsync("UpdatePizzaPrice", _pizzaManager.PIZZA_PRICES[(int)choice]); 
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _pizzaManager.GetGroupName(choice));
        }

        public async Task AddMoney(PizzaChoice choice)
        {
            var evt = _pizzaManager.IncreaseMoney(choice);

            await Clients.Group(_pizzaManager.GetGroupName(choice)).SendAsync("UpdateMoney", evt.Money);
        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            var evt = _pizzaManager.BuyPizza(choice);
            await Clients.Group(_pizzaManager.GetGroupName(choice)).SendAsync("UpdateNbPizzasAndMoney", evt.NbPizzas, evt.Money);
            _pizzaManager.BuyPizza(choice);
        }
    }
}
