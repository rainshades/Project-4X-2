using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    [System.Serializable]
    public struct Squad
    {
        public List<Soldier> Soldiers;
        public int max_soldiers;
    }

    [CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
    public class Unit: ScriptableObject
    {
        public List<Squad> Squads;
        public Sprite UnitCard;
        public GameObject SquadPrefab; 

        public int PowerRanking {
            get {
                int power = 0; 
                foreach (Squad s in Squads)
                {
                    Soldier sample = s.Soldiers[0];
                    power += s.Soldiers.Count;
                    power += Mathf.RoundToInt(sample.attack + sample.defence); 
                    if(sample.armorpen > 0)
                    {
                        power *= 2; 
                    }
                    switch (sample.Type)
                    {
                        case Soldier.SoldierType.light_infintry:
                            power += 1; 
                            break;
                        case Soldier.SoldierType.heavy_infintry:
                            power += 2;
                            break;
                        case Soldier.SoldierType.special_infintry:
                            power += 3; 
                            break;
                        case Soldier.SoldierType.sniper_team:
                            power += 4; 
                            break;
                        case Soldier.SoldierType.light_vehicle:
                            power += 3; 
                            break;
                        case Soldier.SoldierType.heavy_vehicle:
                            power += 5; 
                            break;
                        case Soldier.SoldierType.custom_armor:
                            power += 3;
                            break;
                        case Soldier.SoldierType.other:
                            power += 6; 
                            break;
                    } 
                }
                return power; 
            }
    }


        public void Retrain()
        {
            foreach (Squad squad in Squads)
            {
                for (int i = squad.Soldiers.Count; i <= squad.max_soldiers; i++)
                {
                    squad.Soldiers.Add(squad.Soldiers[0]);
                }
            }
        }
    }
}