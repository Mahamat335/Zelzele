using TMPro;
using UnityEngine.UI;
using Zelzele.Systems.StatSystem;
namespace Zelzele.Systems.DialogueSystem
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public DialogueSO CurrentDialogue;
        public TMP_Text NpcText;
        public Button[] ChoiceButtons;

        private DialogueNode _currentNode;

        void Start()
        {
            StartDialogue(CurrentDialogue);
        }

        public void StartDialogue(DialogueSO dialogue)
        {
            CurrentDialogue = dialogue;
            _currentNode = dialogue.StartNode;
            DisplayNode();
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

