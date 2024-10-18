using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zelzele.Systems.StatSystem
{
    [CreateAssetMenu(fileName = "ModifierSO", menuName = "Scriptable Objects/Stat System/ModifierSO")]
    public class ModifierSO : ScriptableObject
    {
        [SerializeField] private string _modifierName;
        [SerializeField] private string _modifierDescription;
        [SerializeField] public List<StatDictionary> AffectedStats;

        public Modifier GetModifier(string statName)
        {
            foreach (StatDictionary statDictionary in AffectedStats)
            {
                if (statDictionary.StatName.Equals(statName))
                {
                    return new Modifier(_modifierName, _modifierDescription, statDictionary.Value);
                }
            }
            return null;
        }
    }

    [Serializable]
    public struct StatDictionary
    {
        public string StatName;
        public float Value;
    }
}

