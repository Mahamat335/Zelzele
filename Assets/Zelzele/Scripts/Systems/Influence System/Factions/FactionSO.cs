using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Zelzele
{
    [CreateAssetMenu(fileName = "FactionSO", menuName = "Scriptable Objects/FactionSO")]
    public class FactionSO : ScriptableObject
    {
        [SerializeField] private string _factionName;
        [SerializeField, Range(0, 10)] private int _aggressionLevel; // 0-10 arasında, saldırganlık seviyesini belirler.

        public Faction GetFaction()
        {
            return new Faction(_factionName, _aggressionLevel);
        }

    }
}
