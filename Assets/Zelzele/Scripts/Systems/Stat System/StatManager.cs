using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace Zelzele.Systems.StatSystem
{
    public class StatManager : Singleton<StatManager>
    {
        public Dictionary<string, Stat> AllStats;
        public Dictionary<string, List<Modifier>> AllModifiers;
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
            AllStats = new Dictionary<string, Stat>();

            foreach (StatSO statObject in statObjects)
            {
                AllStats[statObject.StatName] = statObject.GetStat();
            }
        }

        private void LoadModifiersFromResources()
        {
            ModifierSO[] modifierObjects = Resources.LoadAll<ModifierSO>(_modifierScriptableObjectsFolderPath);

            AllModifiers = new Dictionary<string, List<Modifier>>();
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
            Dictionary<string, SaveableStatData> statData = new Dictionary<string, SaveableStatData>();

            foreach (Stat stat in AllStats.Values)
            {
                SaveableStatData data = new SaveableStatData
                {
                    BaseValue = stat.BaseValue,
                    Modifiers = stat.Modifiers
                };
                statData.Add(stat.StatName, data);
            }

            string json = JsonUtility.ToJson(new Serialization<string, SaveableStatData>(statData));
            File.WriteAllText(_saveFilePath, json);
        }

        public bool LoadStats()
        {
            if (File.Exists(_saveFilePath))
            {
                string json = File.ReadAllText(_saveFilePath);
                Dictionary<string, SaveableStatData> statData = JsonUtility.FromJson<Serialization<string, SaveableStatData>>(json).ToDictionary();

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

        public bool AddModifierToStat(string statName, string modifierName)
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