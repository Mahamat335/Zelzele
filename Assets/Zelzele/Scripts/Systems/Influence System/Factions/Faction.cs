using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Zelzele
{
    [Serializable]
    public class Faction
    {
        private string _factionName;
        public int _aggressionLevel; // 0-10 arasında, saldırganlık seviyesini belirler.
        public int influence; // Toplam etki puanı.
        public List<Region> controlledRegions; // Kontrol ettiği bölgeler.
        public List<Region> expansionRegions; // Yayılmak istediği bölgeler.
        public SerializableDictionary<Faction, int> relationships; // Diğer factionlarla ilişki seviyesi (-100 ile 100 arasında).

        /* 
                public Faction(string name, int aggression)
                {
                    _factionName = name;
                    _aggressionLevel = aggression;
                    influence = 0;
                    controlledRegions = new List<Region>();
                    expansionRegions = new List<Region>();
                    relationships = new Dictionary<Faction, int>();
                }

                public void ModifyRelationship(Faction otherFaction, int amount)
                {
                    if (relationships.ContainsKey(otherFaction))
                    {
                        relationships[otherFaction] += amount;
                        relationships[otherFaction] = Mathf.Clamp(relationships[otherFaction], -100, 100);
                    }
                    else
                    {
                        relationships.Add(otherFaction, amount);
                    }
                }

                public void AddRegion(Region region)
                {
                    if (!controlledRegions.Contains(region))
                        controlledRegions.Add(region);
                } */
    }
}
