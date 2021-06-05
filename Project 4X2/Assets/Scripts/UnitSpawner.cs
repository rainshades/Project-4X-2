using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Project4X2
{
    public class UnitSpawner : MonoBehaviour
    {
        public static UnitSpawner instance; 

        public GameObject AllySpawnArea, EnemySpawnArea;
        public BattleArmy AllyArmy, EnemyArmy;
        Transform LastSquadSpawned;

        private void Awake()
        {
            instance = this;

            int UnitColumn = 0;
            int UnitRow = 0;
            foreach (Unit unit in BattleTransition.instance.PlayerArmy.Units)
            {   

                foreach (Squad squad in unit.Squads)
                {
                    GameObject go; 

                    if (LastSquadSpawned == null)
                    {
                        go = Instantiate(unit.SquadPrefab, AllySpawnArea.transform.position, Quaternion.identity);
                    }
                    else if(UnitColumn < 5)
                    {
                        go = Instantiate(unit.SquadPrefab, new Vector3(LastSquadSpawned.position.x + 10,
                            LastSquadSpawned.position.y, LastSquadSpawned.position.z), Quaternion.identity);
                    } else
                    {
                        go = Instantiate(unit.SquadPrefab, new Vector3(AllySpawnArea.transform.position.x,
                             LastSquadSpawned.position.y, LastSquadSpawned.position.z - 10), Quaternion.identity);
                        UnitColumn = 0; 
                    }

                    go.GetComponentInChildren<BattleUnit>().Battle_Started = false;
                    go.GetComponent<AIPath>().maxSpeed = squad.Soldiers[0].speed; 
                    go.GetComponentInChildren<BattleUnit>().gameObject.tag = "Player Unit";
                    
                    AllyArmy.battleUnits.Add(go.GetComponentInChildren<BattleUnit>());
                    LastSquadSpawned = go.transform;
                    UnitColumn++; 
                }
            }

            UnitColumn = 0;
            UnitRow = 0; 
            LastSquadSpawned = null; 

            foreach (Unit unit in BattleTransition.instance.EnemyArmy.Units)
            {
                foreach (Squad squad in unit.Squads)
                {
                    GameObject go; 
                    if (LastSquadSpawned == null)
                    {
                        go = Instantiate(unit.SquadPrefab, EnemySpawnArea.transform.position, Quaternion.identity);
                    }
                    else if (UnitColumn < 5)
                    {
                        go = Instantiate(unit.SquadPrefab, new Vector3(LastSquadSpawned.position.x + 10,
                            LastSquadSpawned.position.y, LastSquadSpawned.position.z), Quaternion.identity);
                    }
                    else
                    {
                        go = Instantiate(unit.SquadPrefab, new Vector3(AllySpawnArea.transform.position.x,
                            LastSquadSpawned.position.y, LastSquadSpawned.position.z + 10), Quaternion.identity);
                    
                        UnitColumn = 0;
                    }

                    go.GetComponentInChildren<BattleUnit>().Battle_Started = false;
                    go.GetComponentInChildren<BattleUnit>().gameObject.tag = "Enemy Unit";
                    go.GetComponent<AIPath>().maxSpeed = squad.Soldiers[0].speed;
                    EnemyArmy.battleUnits.Add(go.GetComponentInChildren<BattleUnit>());
                    
                    LastSquadSpawned = go.transform;
                    UnitColumn++; 
                }
            }
        
            ArmyBattleDeckUI.instance.SpawnUnitCards();
        }

        public void StartBattle()
        {
            for (int i = 0; i < AllySpawnArea.transform.childCount; i++)
            {
                transform.GetChild(i).parent = transform;
            }
            for (int i = 0; i < EnemySpawnArea.transform.childCount; i++)
            {
                transform.GetChild(i).parent = transform;
            }

            foreach(BattleUnit unit in FindObjectsOfType<BattleUnit>())
            {
                unit.Battle_Started = true; 
            }

        }
        //Remove units from the list as they die in battle. Remove their cards from the card deck if they were ally cards
        //When one of the armies List is empty the battle is over and you go out to the main menu. 
    }

}