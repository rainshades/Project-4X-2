using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class Army : MonoBehaviour
    {
        public List<Unit> Units;
        public GameObject ArmyCard;

        public Faction Owner;

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
            get { return Units.Count * Units[0].Squads.Count * Units[0].Squads[0].Soldiers.Count; }
        } 
        public void Recruit(Unit unit)
        {
            Units.Add(unit);
        }

        public void Combine(Army SecondArmy)
        {
            SecondArmy.Units.AddRange(Units);
            Destroy(gameObject);
        }

        public void Combine(Settlement Settlement)
        {
        }

        public void Disband(Unit Unit)
        {
            Units.Remove(Unit);
        }
    }


}