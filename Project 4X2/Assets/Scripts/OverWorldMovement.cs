using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class OverWorldMovement : MonoBehaviour
    {
        [SerializeField]
        float max_movement_range = 10; //base movement speed; 

        public float movement_range = 0;

        [SerializeField]
        SpriteRenderer MovementCircle;

        float modifier;// result of additional modifier

        float speedTier = 0;

        private void Awake()
        {

            foreach(Unit unit in GetComponentInParent<Army>().Units)
            {
                foreach(Squad squad in unit.Squads)
                {
                    foreach(Soldier model in squad.Soldiers)
                    {
                        if(model.Type == Soldier.SoldierType.light_infintry || model.Type == Soldier.SoldierType.other 
                            || model.Type == Soldier.SoldierType.special_infintry || model.Type == Soldier.SoldierType.heavy_infintry)
                        {
                            speedTier = 1.0f; 
                        } //Medium Speed Tier
                        else if(model.Type == Soldier.SoldierType.light_vehicle || model.Type == Soldier.SoldierType.custom_armor
                            || model.Type == Soldier.SoldierType.heavy_vehicle)
                        {
                            speedTier = 1.5f; 
                        }//High Speed Tier
                        else if(model.Type == Soldier.SoldierType.heavy_armor)
                        {
                            speedTier = 0.5f; 
                        }//Slow Speed Tier 

                    }
                }
            }
            max_movement_range *= speedTier;
            movement_range = max_movement_range;
            MovementCircle.transform.localScale = new Vector3(max_movement_range,max_movement_range, 1.0f);
        }

        private void Update()
        {
            MovementCircle.transform.localScale = new Vector3(movement_range, movement_range, 1.0f);
        }

        public void MoveCounter(float movement)
        {
            movement_range -= movement; 
        }

        public void TurnOnNotifier()
        {
            MovementCircle.gameObject.SetActive(true); 
        }

        public void TurnOffNotifier()
        {
            MovementCircle.gameObject.SetActive(false);  
        }

        public void resetMovement()
        {
            movement_range = max_movement_range; 
        }

        public void Adjust_Movement(Unit u)
        {
            if (speedTier > 0.5f)
            {
                foreach (Squad s in u.Squads)
                {
                    foreach (Soldier model in s.Soldiers)
                    {
                        if (model.Type == Soldier.SoldierType.light_infintry || model.Type == Soldier.SoldierType.other
                         || model.Type == Soldier.SoldierType.special_infintry || model.Type == Soldier.SoldierType.heavy_infintry)
                        {
                            speedTier = 1.0f;
                        } //Medium Speed Tier
                        else if (model.Type == Soldier.SoldierType.light_vehicle || model.Type == Soldier.SoldierType.custom_armor
                            || model.Type == Soldier.SoldierType.heavy_vehicle)
                        {
                            speedTier = 1.5f;
                        }//High Speed Tier

                    }
                }

                max_movement_range *= speedTier; 
            }
        }

    }
}