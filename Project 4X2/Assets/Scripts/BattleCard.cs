using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Project4X2
{
    public class BattleCard : MonoBehaviour, IPointerClickHandler
    {
        public BattleUnit unit;
        public bool selected; 
        [SerializeField]
        Image UnitArt, Selected;

        public void Select()
        {
            unit.Clicked();
        }
        
        private void Update()
        {
            Selected.gameObject.SetActive(selected);
        }

        void DestroyCard()
        {
            Destroy(gameObject); 
        }

        public void CreateCard(BattleUnit unit)
        {
            this.unit = unit;
            unit.HappensOnDeath += DestroyCard;
            UnitArt.sprite = unit.BaseStats.UnitCard;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked");
            Select();
        }
    }
}