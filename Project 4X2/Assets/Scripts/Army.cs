using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    [System.Serializable]
    public struct ArmyUnits
    {
        public BaseRecruitableUnit Base;
        public List<Squad> Squads;
        public GameObject SquadPrefab;

        public ArmyUnits(BaseRecruitableUnit Base)
        {
            Squads = new List<Squad>();
            Squads.AddRange(Base.Squads); 

            this.Base = Base; this.SquadPrefab = Base.SquadPrefab;
        }
    }


    [System.Serializable]
    public class Army
    {
        public List<ArmyUnits> Units = new List<ArmyUnits>();

        public void Add(BaseRecruitableUnit unit)
        {
            ArmyUnits NewRecruit = new ArmyUnits(unit);
            Units.Add(NewRecruit);
        }

        public void Remove(BaseRecruitableUnit unit)
        {
            ArmyUnits Removable = new ArmyUnits(unit);
            Units.Remove(Removable);
        }
    }
}