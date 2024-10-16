using System.Collections.Generic;

namespace Zelzele.Systems.StatSystem
{
    [System.Serializable]
    public class SaveableStatData
    {
        public float BaseValue;
        public List<Modifier> Modifiers;
    }
}