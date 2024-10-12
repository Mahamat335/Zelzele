using UnityEngine;
using Sigtrap.Relays;

namespace Zelzele.EventSystem
{
    public class EventManager : Singleton<EventManager>
    {
        public Relay ExampleEvent { get; private set; } = new();
    }
}

