using Sigtrap.Relays;

namespace Zelzele.Systems.EventSystem
{
    public class EventManager : Singleton<EventManager>
    {
        public Relay ExampleEvent { get; private set; } = new();
    }
}

