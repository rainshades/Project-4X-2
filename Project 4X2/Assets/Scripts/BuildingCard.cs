using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Project4X2
{
    public class BuildingCard : MonoBehaviour, IPointerClickHandler
    {
        public Building building;
        [SerializeField]
        TextMeshProUGUI text; 

        public void CreateCard()
        {
            text.text = building.name;
            GetComponent<Image>().sprite = building.Sprite; 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Settlement OS = OverWorldSelectManager.Instance.CurrentSelection as Settlement;
            OS.BM.SelectedBuildingSlot.CurrentBuilding = building;
            OS.BM.SelectedBuildingSlot.CreateBuilding();
            OS.BM.BuiltBuildings.Add(building); 
        }
    }
}