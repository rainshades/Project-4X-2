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

        public BuildingSO[] buildings = new BuildingSO[0];

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

        private void Start()
        {
            BM.BuiltBuildings.AddRange(ThisSettlement.buildings);
        }


        public override void Clicked()
        {
            base.Clicked();
            if (tag == "Player")
            {
                OverWorldSelectManager.Instance.CurrentSelection = this;
                OverWorldUIController.Instance.SettlementClicked(this);
            }
            else
            {
                OverWorldSelectManager.Instance.CurrentSelection = null;
            }
        }

        public void Capture(AttatchedArmy faction)
        {
            ThisSettlement.Owner = faction.Owner;
            GetComponent<AttatchedArmy>().Owner = faction.Owner;
            GetComponent<AttatchedArmy>().ArmyNumber = faction.ArmyNumber;
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