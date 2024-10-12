using UnityEngine;

namespace Zelzele.Systems.StatSystem
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "Scriptable Objects/Stat System/StatSO")]
    public class StatSO : ScriptableObject
    {
        [SerializeField] public StatName StatName;
        [SerializeField] private string _statDescription;
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _maxValue;

        public Stat GetStat()
        {
            return new Stat(StatName, _statDescription, _defaultValue, _maxValue);
        }
    }
}