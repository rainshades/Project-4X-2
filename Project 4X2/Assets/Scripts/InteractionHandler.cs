using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class InteractionHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag){

                case "Enemy Unit":
                    GameManager.Instance.LoadBattleScene();
                    break;

                default:
                    Debug.Log("Not an interactable tag");
                    break; 
            }
        }
    }
}