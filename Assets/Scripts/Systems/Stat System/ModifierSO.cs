using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zelzele.Systems.StatSystem
{
    [CreateAssetMenu(fileName = "ModifierSO", menuName = "Scriptable Objects/Stat System/ModifierSO")]
    public class ModifierSO : ScriptableObject
    {
        [SerializeField] private ModifierName ModifierName;
        [SerializeField] private string _modifierDescription;
        [SerializeField] public List<StatDictionary> AffectedStats;

        public Modifier GetModifier(StatName statName)
        {
            foreach (StatDictionary statDictionary in AffectedStats)
            {
                if (statDictionary.StatName.Equals(statName))
                {
                    return new Modifier(ModifierName, _modifierDescription, statDictionary.Value);
                }
            }
            return null;
        }
    }

    [Serializable]
    public struct StatDictionary
    {
        public StatName StatName;
        public float Value;
    }
}

