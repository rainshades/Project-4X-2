using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project4X2
{
    public class BattleArmy : MonoBehaviour
    {
        public List<BattleUnit> battleUnits;
        BattleUI BUI;
        public Dictionary<BaseRecruitableUnit, int> Casualties; 

        private void Awake()
        {
            BUI = FindObjectOfType<BattleUI>();
        }

        private void Update()
        {
            if(battleUnits.Count == 0)
            {
                //The Battle is over and is a victory for the player
                BUI.BattleMenu.gameObject.SetActive(true);
            }
        }

    }
}