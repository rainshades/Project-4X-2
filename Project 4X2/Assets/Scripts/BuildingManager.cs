using UnityEngine;
using System.Collections.Generic;

namespace Project4X2
{
    public class BuildingManager : MonoBehaviour
    {
        public List<Building> PossibleBuildings; 
        public List<BuildingSlot> BuildingSlots;
        public List<Building> BuiltBuildings; 
        public BuildingSlot SelectedBuildingSlot;

        private void Awake()
        {
            PossibleBuildings = GetComponent<Settlement>().Owner.PossibleBuildings; 
        }
    }
}