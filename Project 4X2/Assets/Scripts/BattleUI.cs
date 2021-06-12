using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

namespace Project4X2
{
    public class BattleUI : MonoBehaviour
    {
        public Transform BattleMenu; 

        public void ExitBattleScene()
        {
            BattleTransition.instance.ReturnFromBattle();
            BattleTransition.instance.PostBattle = true; 
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                BattleMenu.gameObject.SetActive(!BattleMenu.gameObject.activeSelf);
            }
        }
    }
}