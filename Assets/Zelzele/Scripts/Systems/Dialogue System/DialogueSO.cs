using UnityEngine;
using Zelzele.Systems.StatSystem;

namespace Zelzele.Systems.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/Dialogue System/DialogueSO")]
    public class DialogueSO : ScriptableObject
    {
        public DialogueNode[] Nodes;
        public Language Language;
    }

    [System.Serializable]
    public class DialogueNode
    {
        public int Id;
        public string NpcText;
        public DialogueChoice[] Choices;
    }

    [System.Serializable]
    public class DialogueChoice
    {
        public string ChoiceText;
        public int NextNodeId;
        public StatRequirement Requirement;
    }

    [System.Serializable]
    public class StatRequirement
    {
        public StatName StatName;
        public int MinValue;
    }

    [System.Serializable]
    public enum Language
    {
        English,
        Turkish
    }
}


