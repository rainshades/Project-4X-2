using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project4X2
{
    public class SettlementBuildingPanel : MonoBehaviour
    {
        [SerializeField]
        GameObject BuildingSlotPrefab; 

        private void OnEnable()
        {
            Settlement OS = OverWorldSelectManager.Instance.CurrentSelection as Settlement;
            int iterator = 0; 
            foreach (BuildingSlot BS in OS.BuildingSlots)
            {
                GameObject go = Instantiate(BuildingSlotPrefab, transform);
                try
                {
                    go.GetComponent<BuildingSlot>().CurrentBuilding = BS.CurrentBuilding;
                }
                catch
                {
                    Debug.Log("Empty  City");
                }
                iterator++;
            }
        }

        private void OnDisable()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}