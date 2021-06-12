using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project4X2
{
    [System.Serializable]
    public class SettlementInfo
    {
        public Faction Owner;

        public bool siege = false;

        public int revenue, influence, order, population;

        public Building[] buildings; 

        public void SyncBuilding(BuildingSlot[] BuildingSlots)
        {
            buildings = new Building[BuildingSlots.Length];
            int index = 0; 
            foreach(BuildingSlot bs in BuildingSlots)
            {
                buildings[index] = bs.building; 
            }
        }
    }

    public class Settlement : Clickable
    {
        public SettlementInfo ThisSettlement = new SettlementInfo();

        public Transform SpawnPoint;

        public bool UnderSiege { get => ThisSettlement.siege; set => ThisSettlement.siege = value; }

        public Faction Owner { get => ThisSettlement.Owner;  }

        public int revenue => ThisSettlement.revenue;
        public int population => ThisSettlement.population;
        public int order => ThisSettlement.order;
        public int influence => ThisSettlement.influence; 

        public BuildingManager BM
        {
            get
            {
                try
                {
                    return  GetComponent<BuildingManager>();
                }
                catch
                {
                    gameObject.AddComponent<BuildingManager>();
                    return GetComponent<BuildingManager>();
                }
            }
        }
        
        public BuildingSlot[] BuildingSlots;

        private void Awake()
        {
            int index = 0; 
            foreach(BuildingSlot bs in BuildingSlots)
            {
                bs.building = ThisSettlement.buildings[index];
            }
        }


        public override void Clicked()
        {
            base.Clicked();
            OverWorldSelectManager.Instance.CurrentSelection = this; 
            OverWorldUIController.Instance.SettlementClicked(this);
        }

        public void Capture(AttatchedArmy faction)
        {
            ThisSettlement.Owner = faction.Owner;
            faction.Combine(GetComponent<AttatchedArmy>().Army);
            Debug.Log("Settlement Captured by" + faction.Owner);
        }

        private void Update()
        {
            if (ThisSettlement.Owner.Player)
            {
                transform.tag = "Player";
            }
            else
            {
                transform.tag = "NPF";
            }

        }

    }

}