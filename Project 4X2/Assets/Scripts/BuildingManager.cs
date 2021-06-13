using UnityEngine;
using System.Collections.Generic;

namespace Project4X2
{
    public class BuildingManager : MonoBehaviour
    {
        public List<BuildingSO> PossibleBuildings; 
        //public List<BuildingSlot> BuildingSlots;
        public List<BuildingSO> BuiltBuildings; 
        public BuildingSlot SelectedBuildingSlot;

        private void Awake()
        {
            PossibleBuildings = GetComponent<Settlement>().Owner.PossibleBuildings; 
        }

        public void SyncSettlementBuildings()
        {
            Settlement Settlement = GetComponent<Settlement>();
            
            Settlement.ThisSettlement.buildings = new BuildingSO[Settlement.BuildingSlots.Length];
            int index = 0;
            foreach (BuildingSO bs in BuiltBuildings)
            {
                if (bs != null)
                {
                    Settlement.ThisSettlement.buildings[index] = bs;
                    index++;
                }
            }
        }
    }
}