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
        public UpdateNbUsersEvent()
        {
            
        }
    }
}
