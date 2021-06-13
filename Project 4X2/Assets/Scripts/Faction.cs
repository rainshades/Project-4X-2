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
        public List<BuildingSO> PossibleBuildings;
        public List<BaseRecruitableUnit> RecruitableUnits; 

        public void NewGame()
        {
            bank = 50000;
            Enemies = new List<Faction>();
            Allies = new List<Faction>();
        }

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