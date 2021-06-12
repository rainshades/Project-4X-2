using UnityEngine;
using System.Collections.Generic; 

namespace Project4X2
{
    public class AttatchedArmy : MonoBehaviour
    {

        public int ArmyNumber;//Maybe change to string later so player can customize army name?

        public Army Army;

        public List<BaseRecruitableUnit> InspectorArmy;

        public GameObject ArmyCard = null;

        public Faction Owner = null;

        void Awake()
        {
            Army = new Army(); 
            
            foreach(BaseRecruitableUnit unit in InspectorArmy)
            {
                Army.Add(unit); 
            }

            try
            {
                if (BattleTransition.instance.PlayerArmyNumber == ArmyNumber)
                {
                    if (BattleTransition.instance.PlayerFaciton == Owner)
                    {
                        Army = BattleTransition.instance.PlayerArmy;
                    }
                }
                if (BattleTransition.instance.EnemyArmyNumber == ArmyNumber)
                {
                    if (BattleTransition.instance.EnemyFaction == Owner)
                    {
                        Army = BattleTransition.instance.EnemyArmy;
                    }
                }
            }
            catch { Debug.LogWarning("Army assignment error"); }
            CheckIfDead();

        }

        public void CheckIfDead()
        {
            bool isDead = false;
            foreach (ArmyUnits units in Army.Units)
            {
                isDead = units.Squads.Count == 0; 
            }
            if (isDead)
            {
                Destroy(gameObject); 
            }
        }

        public void AssignTag(bool ownedByPlayer)
        {
            if (ownedByPlayer)
            {
                tag = "Player Unit";
            }
            else
            {
                tag = "Enemy Unit";
            }
        }

        public int SoliderCount
        {
            get { return Army.Units.Count * Army.Units[0].Squads.Count * Army.Units[0].Squads[0].Soldiers.Count; }
        } 

        public void Recruit(BaseRecruitableUnit unit)
        {
            Army.Add(unit);
        }

        public void Combine(Army SecondArmy)
        {
            SecondArmy.Units.AddRange(Army.Units);
            Destroy(gameObject);
        }

        public void Combine(Settlement Settlement)
        {

        }

        public void Disband(BaseRecruitableUnit Unit)
        {
            Army.Remove(Unit);
        }
    }


}