using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class Recruitment : MonoBehaviour
    {
        public List<Unit> RecruitableUnits;
        [SerializeField]
        Army Army;
        [SerializeField]
        GameObject UnitCardPrefab; 

        public void Recruit(Unit recruit)
        {
            Army.Recruit(recruit); 
        } 

        public void OpenRecruitmentMene()
        {
            if (OverWorldSelectManager.Instance.CurrentSelection is Settlement)
            {
                Settlement ow = OverWorldSelectManager.Instance.CurrentSelection as Settlement;
                Army = ow.GetComponent<Army>();
            }
            else
            {
                OverworldUnit ow = OverWorldSelectManager.Instance.CurrentSelection as OverworldUnit;
                Army = ow.GetComponentInParent<Army>();
            }

            foreach (Unit recruit in RecruitableUnits)
            {
                GameObject go = Instantiate(UnitCardPrefab, transform);
                go.GetComponent<UnitCard>().CreateCard(recruit); 
            }
        }

        public void CloseRecruitmentMenu()
        {
            try
            {
                for (int i = 0; i <= transform.childCount; i++)
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
            catch { } //No Children
        }

        private void OnDisable()
        {
            CloseRecruitmentMenu();
        }
    }
}