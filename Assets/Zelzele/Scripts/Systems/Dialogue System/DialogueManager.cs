using TMPro;
using UnityEngine.UI;
using Zelzele.Systems.StatSystem;
using Zelzele.Systems.EventSystem;
using Zelzele.Player;
using UnityEngine;
using System.Collections.Generic;

namespace Zelzele.Systems.DialogueSystem
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        [SerializeField] private GameObject _dialoguePanel;
        public TMP_Text NpcText;
        public Button[] ChoiceButtons;
        public Language Language;
        public string CurrentDialogueName;
        private DialogueSO _currentDialogue;
        private DialogueNode _currentNode;
        private const string _dialogueScriptableObjectsFolderPath = "Scriptable Objects/Dialogue System/";
        private const string _dialogueSuffix = "_DialogueSO";

        private void OnEnable()
        {
            EventManager.Instance.CurrentDialogueChanged.AddListener(UpdateCurrentDialogue);
            EventManager.Instance.LanguageChanged.AddListener(UpdateLanguage);
        }

        private void OnDisable()
        {
            EventManager.Instance.CurrentDialogueChanged.RemoveListener(UpdateCurrentDialogue);
            EventManager.Instance.LanguageChanged.RemoveListener(UpdateLanguage);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_currentDialogue is not null && _currentDialogue.Language != Language)
            {
                UpdateLanguage(Language);
            }
        }
#endif

        private void UpdateCurrentDialogue(string currentDialogueName)
        {
            _currentDialogue = Resources.Load<DialogueSO>(_dialogueScriptableObjectsFolderPath + Language.ToString() + "/" + currentDialogueName + _dialogueSuffix);

            if (_currentDialogue == null)
            {
                Debug.LogError($"Dialogue not found at path: {_dialogueScriptableObjectsFolderPath}{Language.ToString()}{_dialogueSuffix}. Please make sure the dialogue file exists and the path is correct.");
                return;
            }
        }

        private void UpdateLanguage(Language language)
        {
            Language = language;
            UpdateCurrentDialogue(CurrentDialogueName);
        }

        private void ChangeNode(int id)
        {
            foreach (DialogueNode dialogueNode in _currentDialogue.Nodes)
            {
                if (dialogueNode.Id == id)
                {
                    _currentNode = dialogueNode;
                    return;
                }
            }
        }

        public void StartDialogue(DialogueSO dialogue, int nodeId = 0)
        {
            _currentDialogue = dialogue;
            ChangeNode(nodeId);
            if (NpcText is not null)
            {
                DisplayNode();
            }
        }

        void DisplayNode()
        {
            Cursor.lockState = CursorLockMode.None;
            PlayerManager.Instance.IsMovementLocked = true;
            _dialoguePanel.SetActive(true);
            NpcText.text = _currentNode.NpcText;
            for (int i = 0; i < ChoiceButtons.Length; i++)
            {
                if (_currentNode.Choices.Length <= i)
                {
                    ChoiceButtons[i].gameObject.SetActive(false);
                    continue;
                }

                DialogueChoice choice = _currentNode.Choices[i];
                if (CheckRequirements(choice.Requirements))
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
        }

        public void CloseDialogue()
        {
            Cursor.lockState = CursorLockMode.Locked;
            PlayerManager.Instance.IsMovementLocked = false;
            _currentDialogue = null;
            _dialoguePanel.SetActive(false);
        }

        bool CheckRequirements(List<StatRequirement> statRequirements)
        {
            if (statRequirements.Count == 0)
            {
                return true;
            }

            foreach (StatRequirement statRequirement in statRequirements)
            {
                if (statRequirement.MinValue > StatManager.Instance.AllStats[statRequirement.StatName].CalculatedValue)
                {
                    return false;
                }
            }

            return true;
        }

        void ChooseOption(int index)
        {
            if (_currentNode.Choices[index].NextNodeId == -1)
            {
                CloseDialogue();
                return;
            }

            ChangeNode(_currentNode.Choices[index].NextNodeId);
            DisplayNode();
        }
    }
}

