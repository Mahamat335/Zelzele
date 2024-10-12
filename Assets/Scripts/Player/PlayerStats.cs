using Zelzele.Systems.StatSystem;
using UnityEngine;

namespace Zelzele.Player
{
    public class PlayerStats : MonoBehaviour
    {
        void Start()
        {
            foreach (StatName statName in Stats.Instance.AllStats.Keys)
            {
                Debug.Log(statName);
            }
        }
    }
}

