using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Project4X2
{
    public class OverworldInteractionHandler : MonoBehaviour
    {
        [SerializeField] LayerMask FriendlySettlements;

        public AttatchedArmy OverworldArmy;

        private void Awake()
        {
            OverworldArmy = GetComponentInParent<AttatchedArmy>();
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag){

                case "Enemy Unit":
                    if (GetComponent<OverworldUnit>().AttackMove)
                    {
                        //GameManager.Instance.LoadBattleScene();
                        BattleTransition.instance.PlayerArmy = OverworldArmy.Army;
                        BattleTransition.instance.EnemyArmy = other.GetComponentInParent<AttatchedArmy>().Army;

                        BattleTransition.instance.PlayerArmyNumber = OverworldArmy.ArmyNumber;
                        BattleTransition.instance.EnemyArmyNumber = other.GetComponentInParent<AttatchedArmy>().ArmyNumber;

                        BattleTransition.instance.PlayerFaciton = OverworldArmy.Owner;
                        BattleTransition.instance.EnemyFaction = other.GetComponentInParent<AttatchedArmy>().Owner;

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
                            OverworldArmy.Combine(colliders[0].GetComponent<AttatchedArmy>().Army);
                            OverWorldUIController.Instance.SettlementClicked(colliders[0].GetComponent<Settlement>());
                        }
                    }
                    break;
                case "NPF": //NPF - Non-player faction (city)
                    if (GetComponent<OverworldUnit>().AttackMove)
                    {
                        other.GetComponent<Settlement>().UnderSiege = true;
                        GetComponent<OverworldUnit>().Sieging = true;

                        if(other.GetComponent<AttatchedArmy>().Army.Units.Count < 1)
                        {
                            other.GetComponent<Settlement>().Capture(OverworldArmy);
                        }

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
