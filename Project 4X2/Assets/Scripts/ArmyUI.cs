using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class ArmyUI : MonoBehaviour
    {
        [SerializeField]
        GameObject ArmyCards;

        public List<GameObject> SelectedCards; 

        public void OpenArmyUI(Army ArmyToShow)
        {
            if(transform.childCount > 0)
            {
                RemoveArmy();
            }

            foreach(Unit units in ArmyToShow.Units)
            {
                GameObject go = Instantiate(ArmyCards, transform);
                go.GetComponent<UnitCard>().CreateCard(units);
            }
        }

        public void RemoveArmy()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            SelectedCards.Clear();

        }

    }
}