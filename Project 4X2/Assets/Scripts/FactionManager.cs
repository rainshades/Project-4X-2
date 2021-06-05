using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project4X2
{
    public class FactionManager : MonoBehaviour
    {
        public List<Faction> Factions;
        public Faction PlayerFaction;

        public static FactionManager instance;

        private void Awake()
        {
            instance = this; 
            foreach(Faction faction in Factions)
            {
                if (faction.Player)
                {
                    PlayerFaction = faction; 
                }
            }
        }
    }
}