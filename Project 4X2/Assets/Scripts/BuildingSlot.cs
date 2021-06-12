using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro; 

namespace Project4X2
{
    public class BuildingSlot : MonoBehaviour, IPointerClickHandler
    {
        public BuildingSO CurrentBuilding;
        public Building building; 
        int TurnsToBuild;
        int TurnsLeft; 
        public TextMeshProUGUI TurnsLeftText;

        private void Awake()
        {
            if (CurrentBuilding != null)
            {
                building = CurrentBuilding.Building;

                TurnsToBuild = building.turns_to_build;
                TurnsLeftText = GetComponentInChildren<TextMeshProUGUI>();
                TurnsLeftText.text = "" + TurnsLeft;
            }
            else
            {
                TurnsLeftText = GetComponentInChildren<TextMeshProUGUI>();
                TurnsLeftText.text = "";
            }
        }

        public void CreateBuildingSprite() {

            GetComponent<Image>().sprite = CurrentBuilding.Building.Sprite;
            if (building != null)
            {
                building = CurrentBuilding.Building;
            }
        }

        public void EndTurn()
        {
            TurnsLeft--; 
            if(TurnsLeft <= 0)
            {
                TurnsLeftText.gameObject.SetActive(false);
                building.enabled = true; 
            }
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