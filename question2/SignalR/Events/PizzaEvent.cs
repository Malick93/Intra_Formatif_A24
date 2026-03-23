using System.Numerics;

namespace SignalR.Events
{
    public abstract class PizzaEvent
    {
        public abstract string EventType { get; }
        public List<PizzaEvent> Events { get; set; }
    }
    public class UpdateNbUsersEvent : PizzaEvent
    {
        public override string EventType { get { return "UpdateNbUsers"; } }
        public int NbUsers { get; set; }
        public UpdateNbUsersEvent(int nbUsers)
        {
            NbUsers = nbUsers;
        }
    }
    public class UpdateMoneyEvent : PizzaEvent
    {
        public override string EventType { get { return "UpdateMoneyEvent"; } }
        public int Money { get; set; }
        public UpdateMoneyEvent(int money)
        {
            Money = money;
        }
    }
    public class UpdateNbPizzasAndMoney : PizzaEvent
    {
        public override string EventType { get { return "UpdateNbPizzasAndMoney"; } }
        public int NbPizzas {  get; set; }
        public int Money { get; set; }
        public UpdateNbPizzasAndMoney(int nbPizzas, int money)
        {
            NbPizzas = nbPizzas;
            Money = money;
        }
    }
    public class UpdatePizzaPrice : PizzaEvent
    {
        public override string EventType { get { return "UpdatePizzaPrice"; } }
        public int PizzaPrice { get; set; }
        public UpdatePizzaPrice(int pizzaPrice)
        {
            PizzaPrice = pizzaPrice;
        }
    }
    
}
