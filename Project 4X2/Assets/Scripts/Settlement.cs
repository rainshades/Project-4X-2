using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project4X2
{
    public class Settlement : Clickable
    {
        public Faction Owner;

        bool siege = false;

        public bool UnderSiege { get => siege; set => siege = value; }

        public Transform SpawnPoint;

        public int revenue, influence, order, population;

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

        public override void Clicked()
        {
            base.Clicked();
            OverWorldSelectManager.Instance.CurrentSelection = this; 
            OverWorldUIController.Instance.SettlementClicked(this);
        }

        private void Update()
        {
            if (Owner.Player)
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