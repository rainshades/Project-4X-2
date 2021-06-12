using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Project4X2
{
    public class BuildingCard : MonoBehaviour, IPointerClickHandler
    {
        public BuildingSO building;
        [SerializeField]
        TextMeshProUGUI text; 

        public void CreateCard()
        {
            text.text = building.name;
            GetComponent<Image>().sprite = building.Building.Sprite; 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Settlement OS = OverWorldSelectManager.Instance.CurrentSelection as Settlement;
            OS.BM.SelectedBuildingSlot.CurrentBuilding = building;
            OS.BM.SelectedBuildingSlot.CreateBuildingSprite();
            OS.BM.BuiltBuildings.Add(building); 
        }
    }
}