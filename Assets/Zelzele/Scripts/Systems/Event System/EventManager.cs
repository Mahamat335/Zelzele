using Sigtrap.Relays;
using Zelzele.Systems.DialogueSystem;

namespace Zelzele.Systems.EventSystem
{
    public class EventManager : Singleton<EventManager>
    {
        #region Dialogue System Events
        public Relay<string> CurrentDialogueChanged { get; private set; } = new();
        public Relay<Language> LanguageChanged { get; private set; } = new();
        #endregion
    }
}

