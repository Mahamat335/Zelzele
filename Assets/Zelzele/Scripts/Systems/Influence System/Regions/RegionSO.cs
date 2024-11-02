using UnityEngine;

namespace Zelzele
{
    [CreateAssetMenu(fileName = "RegionSO", menuName = "Scriptable Objects/RegionSO")]
    public class RegionSO : ScriptableObject
    {
        public string RegionName;
        [SerializeField] private SerializableDictionary<Faction, int> _defaultFactionInfluences;
    }
}
