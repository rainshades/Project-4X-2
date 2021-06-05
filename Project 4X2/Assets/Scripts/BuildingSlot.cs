using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Project4X2
{
    public class BuildingSlot : MonoBehaviour, IPointerClickHandler
    {
        public Building CurrentBuilding; 

        public void CreateBuilding() {

            GetComponent<Image>().sprite = CurrentBuilding.Sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(CurrentBuilding == null)
            {
                OverWorldUIController.Instance.ShowBuildingDeck();
                Settlement OS = OverWorldSelectManager.Instance.CurrentSelection as Settlement;
                OS.BM.SelectedBuildingSlot = this; 
            }
            else
            {
                //Building Options like destroy building, building details, etc. 
            }
        }
    }
}