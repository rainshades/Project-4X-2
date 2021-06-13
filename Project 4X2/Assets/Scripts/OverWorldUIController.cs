using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Project4X2
{
    public class OverWorldUIController : MonoBehaviour
    {
        public static OverWorldUIController Instance;

        [SerializeField]
        GameObject BuildingCardPrefab; 

        [SerializeField]
        GameObject SettlementDeck;
        [SerializeField]
        GameObject ArmyDeck;
        [SerializeField]
        GameObject BuildingPanel;

        void Awake()
        {
            Instance = this;
            SettlementDeck.SetActive(false);
        }

        public void OpenArmyTab(ArmyUI armyUI)
        {
            if(OverWorldSelectManager.Instance.CurrentSelection is Settlement)
            {
                Settlement ow = OverWorldSelectManager.Instance.CurrentSelection as Settlement;
                armyUI.OpenArmyUI(ow.GetComponent<AttatchedArmy>().Army);
            }
            else
            {
                OverworldUnit ow = OverWorldSelectManager.Instance.CurrentSelection as OverworldUnit;
                armyUI.OpenArmyUI(ow.GetComponentInParent<AttatchedArmy>().Army);
            }
        }

        private void ShowSettlementDeck()
        {
            SettlementDeck.SetActive(true);
        }
        private void ShowArmyDeck()
        {
            ArmyDeck.SetActive(true);
        }

        public void ShowBuildingDeck()
        {
            BuildingPanel.SetActive(true);

            if(BuildingPanel.transform.childCount == 0)
            {
                Settlement ow = OverWorldSelectManager.Instance.CurrentSelection as Settlement;
                foreach (BuildingSO b in ow.BM.PossibleBuildings)
                {
                    GameObject go = Instantiate(BuildingCardPrefab, BuildingPanel.transform);
                    go.GetComponent<BuildingCard>().building = b;
                    go.GetComponent<BuildingCard>().CreateCard();
                }
            }

        }

        public void HideDeck()
        {
            SettlementDeck.SetActive(false);
            ArmyDeck.SetActive(false);
            BuildingPanel.SetActive(false); 
        }

        public void SettlementClicked(Settlement settlement)
        {
            HideDeck();
            ShowSettlementDeck();
            transform.GetChild(1).gameObject.SetActive(false); 
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }

        public void ArmyClicked(OverworldUnit unit)
        {
            HideDeck();
            ShowArmyDeck();
            transform.GetChild(0).gameObject.SetActive(false);  
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
    }
}