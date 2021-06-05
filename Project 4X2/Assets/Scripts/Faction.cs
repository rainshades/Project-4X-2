using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    [CreateAssetMenu(fileName = "New Faction", menuName = "Faction")]
    public class Faction : ScriptableObject
    {
        
        public bool Player; 

        public int bank;
        public int income;

        public List<Faction> Enemies;
        public List<Faction> Allies;

        public List<Settlement> Territory;
        public List<Building> PossibleBuildings; 

        public void CalculateIncome()
        {
            foreach(Settlement city in Territory)
            {
                income += city.revenue; 
            }
        }

        public void GainRevenue()
        {
            bank += income; 
        }
    }
}