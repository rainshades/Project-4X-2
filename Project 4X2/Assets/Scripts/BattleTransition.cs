using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project4X2
{
    public class BattleTransition : MonoBehaviour
    {
        public static BattleTransition instance;

        public int PlayerArmyNumber, EnemyArmyNumber;
        public Faction PlayerFaciton, EnemyFaction; 

        public Army PlayerArmy;
        public Army EnemyArmy;

        private List<ArmyUnits> PABattle; // Use to hold player army data inbetween battles
        private List<ArmyUnits> EABattle; // Use to hold None player army data inbetween battles

        public bool PostBattle; 

        private void Awake()
        {
            instance = this;
            if (FindObjectsOfType<BattleTransition>().Length > 1)
            {
                Destroy(FindObjectsOfType<BattleTransition>()[1].gameObject);
            }
            DontDestroyOnLoad(gameObject); 
        }

        public void UnitDies(BattleUnit Unit)
        {
            if(Unit.gameObject.tag == "Player Unit")
            {
                PABattle[Unit.UnitNumber].Squads.RemoveAt(0);
                
            } else if(Unit.gameObject.tag == "Enemy Unit")
            {
                EABattle[Unit.UnitNumber].Squads.RemoveAt(0);
            }
        }

        public void ShowMatchupMenu(GameObject Parent)
        {
            GameObject go;

            for(int i = 0; i < Parent.transform.childCount; i++)
            {
                Parent.transform.GetChild(i).gameObject.SetActive(true); 
            }
            
            foreach (ArmyUnits unit in PlayerArmy.Units)
            {
                go = new GameObject();
                go.transform.parent = Parent.transform.GetChild(0); 
                Image Cardart = go.AddComponent<Image>();
                Cardart.sprite = unit.Base.UnitCard; 
            }

            foreach(ArmyUnits unit in EnemyArmy.Units)
            {
                go = new GameObject();
                go.transform.parent = Parent.transform.GetChild(1);
                Image Cardart = go.AddComponent<Image>();
                Cardart.sprite = unit.Base.UnitCard;
            }

            PABattle = PlayerArmy.Units;
            EABattle = EnemyArmy.Units;
        }

        public void PostMatchMenu(GameObject Parent)
        {
            GameObject go;

            for (int i = 0; i < Parent.transform.childCount; i++)
            {
                Parent.transform.GetChild(i).gameObject.SetActive(true);
            }

            foreach (ArmyUnits unit in PlayerArmy.Units)
            {
                go = new GameObject();
                go.transform.parent = Parent.transform.GetChild(0);
                Image Cardart = go.AddComponent<Image>();
                Cardart.sprite = unit.Base.UnitCard;

                if(unit.Squads.Count == 0)
                {
                    Cardart.color = new Color(Cardart.color.r, Cardart.color.g, Cardart.color.b, 0.5f);
                }

            }

            foreach (ArmyUnits unit in EnemyArmy.Units)
            {
                go = new GameObject();
                go.transform.parent = Parent.transform.GetChild(1);
                Image Cardart = go.AddComponent<Image>();
                Cardart.sprite = unit.Base.UnitCard;
            }
        }

        public void Battle()
        {
            GameManager.Instance.LoadBattleScene();
        }


        public void ReturnFromBattle()//Army A, Army B)
        {
            GameManager.Instance.LoadOverworldScene();
            PlayerArmy.Units = PABattle;
            EnemyArmy.Units = EABattle; 
        }
    }
}