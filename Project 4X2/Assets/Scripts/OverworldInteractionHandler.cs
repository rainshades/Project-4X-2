using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Project4X2
{
    public class OverworldInteractionHandler : MonoBehaviour
    {
        [SerializeField] LayerMask FriendlySettlements; 

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag){

                case "Enemy Unit":
                    if (GetComponent<OverworldUnit>().AttackMove)
                    {
                        //GameManager.Instance.LoadBattleScene();
                        BattleTransition.instance.PlayerArmy = GetComponentInParent<Army>();
                        BattleTransition.instance.EnemyArmy = other.GetComponentInParent<Army>();
                        BattleTransition.instance.ShowMatchupMenu(MatchupUI.instance.gameObject);
                        GetComponent<OverworldUnit>().AttackMove = false; 
                    }
                    break;
                case "Player":
                    if(GetComponent<OverworldUnit>().CombineMove)
                    {
                        if (Physics.CheckSphere(transform.position, 1.0f, FriendlySettlements))
                        {
                            Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f, FriendlySettlements);
                            GetComponentInParent<Army>().Combine(colliders[0].GetComponent<Army>());
                            OverWorldUIController.Instance.SettlementClicked(colliders[0].GetComponent<Settlement>());
                        }
                    }
                    break;
                case "NPF":
                    if (GetComponent<OverworldUnit>().AttackMove)
                    {
                        other.GetComponent<Settlement>().UnderSiege = true;
                        GetComponent<OverworldUnit>().Sieging = true;
                    }
                    break; 
                default:
                    Debug.Log("Not an interactable tag");
                    break; 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (other.tag)
            {
                case "NPF":
                    other.GetComponent<Settlement>().UnderSiege = false;
                    GetComponent<OverworldUnit>().Sieging = false;
                    break; 
            }
        }
    }
}
