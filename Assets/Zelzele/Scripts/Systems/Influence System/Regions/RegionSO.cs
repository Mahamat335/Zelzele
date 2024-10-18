using UnityEngine;
using System.Collections.Generic;

namespace Zelzele
{
    [CreateAssetMenu(fileName = "RegionSO", menuName = "Scriptable Objects/RegionSO")]
    public class RegionSO : ScriptableObject
    {
        public string RegionName;
        [SerializeField] private SerializableDictionary<Faction, int> _factionInfluences;
        [SerializeField] private Faction dominantFaction;

        public Region(string name)
        {
            regionName = name;
            factionInfluences = new Dictionary<Faction, int>();
            dominantFaction = null;
        }

        public void UpdateDominantFaction()
        {
            Faction maxFaction = null;
            int maxInfluence = -1;

            foreach (var kvp in factionInfluences)
            {
                if (kvp.Value > maxInfluence)
                {
                    maxInfluence = kvp.Value;
                    maxFaction = kvp.Key;
                }
            }

            dominantFaction = maxFaction;
        }

        public void ModifyInfluence(Faction faction, int amount)
        {
            if (factionInfluences.ContainsKey(faction))
            {
                factionInfluences[faction] += amount;
            }
            else
            {
                factionInfluences.Add(faction, amount);
            }

            UpdateDominantFaction();
        }
    }
}
