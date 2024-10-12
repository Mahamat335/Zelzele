using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zelzele.Systems.StatSystem
{
    public class Stats : Singleton<Stats>
    {
        public Dictionary<StatName, Stat> AllStats;
        public Dictionary<StatName, List<Modifier>> AllModifiers;
        private const string _statScriptableObjectsFolderPath = "Scriptable Objects/Stat System/Stats";
        private const string _modifierScriptableObjectsFolderPath = "Scriptable Objects/Stat System/Modifiers";
        private string _saveFilePath;

        private void OnEnable()
        {
            _saveFilePath = Application.persistentDataPath + "/SavedFiles/Stats/stats.json";
            LoadStatsFromResources();
            LoadModifiersFromResources();
            LoadStats();
        }

        #region File Operations
        private void LoadStatsFromResources()
        {
            StatSO[] statObjects = Resources.LoadAll<StatSO>(_statScriptableObjectsFolderPath);
            AllStats = new Dictionary<StatName, Stat>();

            foreach (StatSO statObject in statObjects)
            {
                AllStats[statObject.StatName] = statObject.GetStat();
            }
        }

        private void LoadModifiersFromResources()
        {
            ModifierSO[] modifierObjects = Resources.LoadAll<ModifierSO>(_modifierScriptableObjectsFolderPath);

            AllModifiers = new Dictionary<StatName, List<Modifier>>();
            for (int i = 0; i < modifierObjects.Length; i++)
            {
                foreach (StatDictionary affectedStat in modifierObjects[i].AffectedStats)
                {
                    if (AllModifiers.ContainsKey(affectedStat.StatName))
                    {
                        AllModifiers[affectedStat.StatName].Add(modifierObjects[i].GetModifier(affectedStat.StatName));
                    }
                    else
                    {
                        AllModifiers[affectedStat.StatName] = new List<Modifier>();
                    }
                }
            }
        }

        public void SaveStats()
        {
            Dictionary<StatName, SaveableStatData> statData = new Dictionary<StatName, SaveableStatData>();

            foreach (Stat stat in AllStats.Values)
            {
                SaveableStatData data = new SaveableStatData
                {
                    BaseValue = stat.BaseValue,
                    Modifiers = stat.Modifiers
                };
                statData.Add(stat.StatName, data);
            }

            string json = JsonUtility.ToJson(new Serialization<StatName, SaveableStatData>(statData));
            File.WriteAllText(_saveFilePath, json);
        }

        public bool LoadStats()
        {
            if (File.Exists(_saveFilePath))
            {
                string json = File.ReadAllText(_saveFilePath);
                Dictionary<StatName, SaveableStatData> statData = JsonUtility.FromJson<Serialization<StatName, SaveableStatData>>(json).ToDictionary();

                foreach (Stat stat in AllStats.Values)
                {
                    if (statData.ContainsKey(stat.StatName))
                    {
                        stat.BaseValue = statData[stat.StatName].BaseValue;
                        stat.Modifiers = statData[stat.StatName].Modifiers;
                    }
                }
                return true;
            }
            return false;
        }
        #endregion

        public bool AddModifierToStat(StatName statName, ModifierName modifierName)
        {
            if (AllModifiers.ContainsKey(statName))
            {
                foreach (Modifier modifier in AllModifiers[statName])
                {
                    if (modifierName.Equals(modifier.ModifierName))
                    {
                        if (AllStats.ContainsKey(statName))
                        {
                            AllStats[statName].Modifiers.Add(modifier);
                            return true;
                        }

                        Debug.Log("Failed Function: AddModifierToStat. Invalid Stat Name! All Stats List Does not contain: " + statName);
                        return false;
                    }
                }

                Debug.Log("Failed Function: AddModifierToStat. Invalid Modifier Name! All Modifiers does not contain: " + modifierName + " for " + statName);
                return false;
            }

            Debug.Log("Failed Function: AddModifierToStat. Invalid Stat Name! All Modifiers does not contain: " + statName);
            return false;
        }
    }
}