using UnityEngine;
using Zelzele.Systems.DialogueSystem;

namespace Zelzele.Systems.EntitySystem
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private string _npcName;
        [SerializeField] private DialogueSO _npcDialogue;

        //TODO buraya enter exit yazip bir de e tusunu da eventle hallettigimizde daha optimize olacak
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerAction();
            }
        }

        private void TriggerAction()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.Instance.StartDialogue(_npcDialogue);
            }
        }
    }
}
