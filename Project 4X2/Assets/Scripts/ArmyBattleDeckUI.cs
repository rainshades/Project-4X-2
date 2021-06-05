using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class ArmyBattleDeckUI : MonoBehaviour
    {
        List<BattleUnit> Army;
        public GameObject CardPrefab;
        public List<GameObject> Cards; 

        public static ArmyBattleDeckUI instance;

        private void Awake()
        {
            instance = this; 
        }

        public void SpawnUnitCards()
        {
            Army = PlayerArmy(); 

            foreach (BattleUnit unit in Army)
            {
                GameObject go = Instantiate(CardPrefab, transform);
                go.GetComponent<BattleCard>().CreateCard(unit);
                Cards.Add(go); 
            }
        }

        public void Despawn(BattleUnit Unit)
        {
            GameObject Card; 
            foreach(GameObject go in Cards)
            {
                if(go.GetComponent<BattleCard>().unit = Unit)
                {
                    Card = go; 
                    Cards.Remove(Card);
                    Destroy(Card);
                    return; 
                }
            }
        }

        public List<BattleUnit> PlayerArmy()
        {
            List<BattleUnit> unitList = new List<BattleUnit>();

            foreach(BattleUnit unit in FindObjectsOfType<BattleUnit>())
            {
                if(unit.transform.tag == "Player Unit")
                {
                    unitList.Add(unit); 
                }
            }

            return unitList;
        }
    }
}