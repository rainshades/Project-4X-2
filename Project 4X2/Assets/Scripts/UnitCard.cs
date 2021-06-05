using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Project4X2
{
    public class UnitCard : MonoBehaviour, IPointerClickHandler
    {
        public Unit recruit;
        protected bool selected;
        [SerializeField]
        protected Image UnitArt, Selected;

        private void Awake()
        {
            Selected.gameObject.SetActive(false);
        }

        private void Update()
        {
            Selected.gameObject.SetActive(selected); 
        }

        public virtual void CreateCard(Unit recruit)
        {
            this.recruit = recruit; 
            UnitArt.sprite = recruit.UnitCard;
            GetComponentInChildren<TextMeshProUGUI>().text = "" + recruit.Squads.Count;
        }

        public void RecruitButton()
        {
            GetComponentInParent<Recruitment>().Recruit(recruit); 
        }

        public virtual void Select()
        {
            ArmyUI ui = GetComponentInParent<ArmyUI>();
            if (ui != null)
            {
                ui.SelectedCards.Add(this.gameObject);
                selected = !selected;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select(); 
        }

    }
}