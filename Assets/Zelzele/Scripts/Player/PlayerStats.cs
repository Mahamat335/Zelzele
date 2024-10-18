using Zelzele.Systems.StatSystem;
using UnityEngine;

namespace Zelzele.Player
{
    public class PlayerStats : MonoBehaviour
    {
        void Start()
        {
            foreach (string statName in StatManager.Instance.AllStats.Keys)
            {
                Debug.Log(statName);
            }
        }
    }
}

