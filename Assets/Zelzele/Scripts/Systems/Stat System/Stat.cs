using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zelzele.Systems.StatSystem
{
    [Serializable]
    public class Stat
    {
        [field: SerializeField]
        public StatName StatName { get; set; }
        [field: SerializeField]
        public string StatDescription { get; set; }
        [field: SerializeField]
        public float BaseValue { get; set; }
        [field: SerializeField]
        public float MaxValue { get; set; }
        [field: SerializeField]
        public List<Modifier> Modifiers { get; set; }
        public float CalculatedValue
        {
            get
            {
                float totalModification = 0;
                foreach (Modifier modifier in Modifiers)
                {
                    totalModification += modifier.ModifierValue;
                }
                return BaseValue + totalModification;
            }
        }

        public Stat(StatName statName, string statDescription, float baseValue, float maxValue, float bonusValue)
        {
            StatName = statName;
            StatDescription = statDescription;
            BaseValue = baseValue;
            MaxValue = maxValue;
            Modifiers = new List<Modifier>();
        }

        public Stat(StatName statName, string statDescription, float defaultValue, float maxValue)
        {
            StatName = statName;
            StatDescription = statDescription;
            BaseValue = defaultValue;
            MaxValue = maxValue;
            Modifiers = new List<Modifier>();
        }

        public bool IncreaseBaseValue(float incrementValue, bool isForced = false)
        {
            if (BaseValue + incrementValue > MaxValue)
            {
                if (isForced == true)
                {
                    BaseValue = MaxValue;
                }
                return false;
            }

            BaseValue += incrementValue;
            return true;
        }

        public bool DecreaseBaseValue(float decrementValue, bool isForced = false)
        {
            if (BaseValue - decrementValue < 0)
            {
                if (isForced == true)
                {
                    BaseValue = 0;
                }
                return false;
            }

            BaseValue -= decrementValue;
            return true;
        }
    }
}

