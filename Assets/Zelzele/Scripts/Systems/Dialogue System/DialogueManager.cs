using TMPro;
using UnityEngine.UI;
using Zelzele.Systems.StatSystem;
using System.IO;
using UnityEngine;

namespace Zelzele.Systems.DialogueSystem
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public DialogueSO CurrentDialogue;
        public TMP_Text NpcText;
        public Button[] ChoiceButtons;
        public Language Language;

        private DialogueNode _currentNode;
        private const string _languageScriptableObjectsFolderPath = "Scriptable Objects/Dialogue System/";
        private const string _languageSuffix = "_DialogueSO";

        void Start()
        {
            Debug.Log(_languageScriptableObjectsFolderPath + Language.ToString());
            CurrentDialogue = Resources.Load<DialogueSO>(_languageScriptableObjectsFolderPath + Language.ToString() + _languageSuffix);
            StartDialogue(CurrentDialogue);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (CurrentDialogue is not null && CurrentDialogue.Language != Language)
            {
                CurrentDialogue = Resources.Load<DialogueSO>(_languageScriptableObjectsFolderPath + Language.ToString() + _languageSuffix);
            }
        }
#endif

        public void StartDialogue(DialogueSO dialogue)
        {
            CurrentDialogue = dialogue;
            _currentNode = dialogue.StartNode;
            if (NpcText is not null)
            {
                DisplayNode();
            }
        }

        void DisplayNode()
        {
            NpcText.text = _currentNode.NpcText;
            for (int i = 0; i < ChoiceButtons.Length; i++)
            {
                if (i < _currentNode.Choices.Length)
                {
                    var choice = _currentNode.Choices[i];
                    if (CheckRequirements(choice.Requirement))
                    {
                        ChoiceButtons[i].GetComponentInChildren<TMP_Text>().text = choice.ChoiceText;
                        ChoiceButtons[i].gameObject.SetActive(true);
                        int index = i;
                        ChoiceButtons[i].onClick.RemoveAllListeners();
                        ChoiceButtons[i].onClick.AddListener(() => ChooseOption(index));
                    }
                    else
                    {
                        ChoiceButtons[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    ChoiceButtons[i].gameObject.SetActive(false);
                }
            }
        }

        bool CheckRequirements(StatRequirement statRequirement)
        {
            if (statRequirement == null)
            {
                return true;
            }

            if (statRequirement.MinValue <= StatManager.Instance.AllStats[statRequirement.StatName].CalculatedValue)
            {
                return true;
            }

            return false;
        }

        void ChooseOption(int index)
        {
            _currentNode = _currentNode.Choices[index].NextNode;
            DisplayNode();
        }
    }
}

