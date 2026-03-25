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
            _pizzaManager.AddUser();

            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _pizzaManager.RemoveUser();

            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            string groupName = _pizzaManager.GetGroupName(choice);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Caller.SendAsync("UpdatePizzaPrice", _pizzaManager.PIZZA_PRICES[(int)choice]);

            await Clients.Group(groupName).SendAsync(
                "UpdateNbPizzasAndMoney",
                _pizzaManager.NbPizzas[(int)choice],
                _pizzaManager.Money[(int)choice]
            );
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            string groupName = _pizzaManager.GetGroupName(choice);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task AddMoney(PizzaChoice choice)
        {
            _pizzaManager.IncreaseMoney(choice);

            string groupName = _pizzaManager.GetGroupName(choice);

            await Clients.Group(groupName).SendAsync(
                "UpdateMoney",
                _pizzaManager.Money[(int)choice]
            );
        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            _pizzaManager.BuyPizza(choice);

            string groupName = _pizzaManager.GetGroupName(choice);

            await Clients.Group(groupName).SendAsync(
                "UpdateNbPizzasAndMoney",
                _pizzaManager.NbPizzas[(int)choice],
                _pizzaManager.Money[(int)choice]
            );
        }
    }
}
