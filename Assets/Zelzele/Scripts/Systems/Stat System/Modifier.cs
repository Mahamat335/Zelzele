using System;
using UnityEngine;

namespace Zelzele.Systems.StatSystem
{
    [Serializable]
    public class Modifier
    {
        [field: SerializeField]
        public ModifierName ModifierName { get; set; }
        [field: SerializeField]
        public string ModifierDescription { get; set; }
        [field: SerializeField]
        public float ModifierValue { get; set; }

        public Modifier(ModifierName modifierName, string modifierDescription, float modifierValue)
        {
            ModifierName = modifierName;
            ModifierDescription = modifierDescription;
            ModifierValue = modifierValue;
        }
    }
}