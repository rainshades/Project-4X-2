using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Project4X2
{
    public class BattleTransition : MonoBehaviour
    {
        public static BattleTransition instance; 

        public Army PlayerArmy;
        public Army EnemyArmy;

        private List<Unit> PABattle; // Use to hold player army data inbetween battles
        private List<Unit> EABattle; // Use to hold None player army data inbetween battles

        private void Awake()
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); 
        }

        public void ShowMatchupMenu(GameObject Parent)
        {
            GameObject go;

            for(int i = 0; i < Parent.transform.childCount; i++)
            {
                Parent.transform.GetChild(i).gameObject.SetActive(true); 
            }
            
            foreach (Unit unit in PlayerArmy.Units)
            {
                go = new GameObject();
                go.transform.parent = Parent.transform.GetChild(0); 
                Image Cardart = go.AddComponent<Image>();
                Cardart.sprite = unit.UnitCard; 
            }

            foreach(Unit unit in EnemyArmy.Units)
            {
                go = new GameObject();
                go.transform.parent = Parent.transform.GetChild(1);
                Image Cardart = go.AddComponent<Image>();
                Cardart.sprite = unit.UnitCard;
            }

            PABattle = PlayerArmy.Units;
            EABattle = EnemyArmy.Units;
        }

        public void Battle()
        {
            //Save Overworld Scene State 
            GameManager.Instance.LoadBattleScene();
        }


        public void ReturnFromBattle()//Army A, Army B)
        {
            GameManager.Instance.LoadOverworldScene();
        }

    }

}