using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project4X2
{
    public class MatchupUI : MonoBehaviour
    {
        public GameObject PlayerSide, EnemySide;

        public static MatchupUI instance;
        private void Awake()
        {
            instance = this;

            if (BattleTransition.instance.PostBattle)
            {
                BattleTransition.instance.PostMatchMenu(gameObject);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(true);
            }
        }

        public void StartBattle()
        {
            BattleTransition.instance.Battle(); 
        }

        public void CloseStatsWindow()
        {
            BattleTransition.instance.PostBattle = false; 
            gameObject.SetActive(false); 
        }
    }
}