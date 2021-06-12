using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Project4X2
{
    public class BattleUnit : Clickable
    { //So far everything here only applies to simple infintry 

        public delegate void OnDeath();

        public OnDeath HappensOnDeath;

        public int UnitNumber, SquadNumber; 
        public BaseRecruitableUnit BaseStats;
        bool fired = false;
        public bool Enemy, Battle_Started;
        public BattleUnitAnimationController Buac; 
        public bool IsEnemy() { return transform.tag == "Enemy" || transform.tag == "Enemy Unit" ;  }

        AIDestinationSetter destination; 

        public AIDestinationSetter Destination { get => destination; }

        [SerializeField]
        BattleUnit AttackTarget;
        [SerializeField]
        LayerMask EnemyLayer;

        float MaxHealth, health, reload_speed, reload_timer;

        ///float implementations still needed:
        ///Moral,fetigue, ammo, armor lvl, 

        private void Awake()
        {
            Buac = GetComponent<BattleUnitAnimationController>();
            MaxHealth = BaseStats.Squads[0].Soldiers[0].health * BaseStats.Squads[0].Soldiers.Count;
            health = MaxHealth; 

            reload_speed = BaseStats.Squads[0].Soldiers[0].reload_Speed;
            reload_timer = reload_speed; 

            HappensOnDeath = new OnDeath(Die); 

            destination = GetComponent<AIDestinationSetter>();

        }

        private void Update()
        {
            AttackTarget = GetNearbyUnits();

            if (AttackTarget != null)
            {
                if (transform.tag == "Player Unit")
                {
                    if (AttackTarget.tag == "Enemy Unit" && reload_timer == reload_speed)
                    {
                        Attack(AttackTarget);
                    }
                    else
                    {
                        AttackTarget = null;
                    }
                }
                else if (transform.tag == "Enemy Unit")
                {
                    if (AttackTarget.tag == "Player Unit" && reload_timer == reload_speed)
                    {
                        Attack(AttackTarget);
                    }
                    else
                    {
                        AttackTarget = null;
                    }
                }
            }

            if(reload_timer > 0 && fired)
            {
                reload_timer -= Time.deltaTime; 
            } else if(reload_timer <= 0)
            {
                fired = false;
                reload_timer = reload_speed;
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage; 

            if(health <= 0)
            {
                HappensOnDeath.Invoke(); 
            }
        }

        public void Die()
        {
            //gameObject.SetActive(false);
            if (transform.tag == "Player Unit")
            {
                BattleTransition.instance.UnitDies(this);
                UnitSpawner.instance.AllyArmy.battleUnits.Remove(this);
            }
            else if (transform.tag == "Enemy Unit")
            {
                BattleTransition.instance.UnitDies(this); 
                UnitSpawner.instance.EnemyArmy.battleUnits.Remove(this);
            }
            Destroy(gameObject); 
        }

        BattleUnit GetNearbyUnits()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.parent.transform.position, BaseStats.Squads[0].Soldiers[0].range, EnemyLayer);
            foreach (Collider col in colliders)
            {
                try
                {
                    col.transform.parent.TryGetComponent(out BattleUnit BU);
                    if (BU.transform.tag == "Player Unit" && transform.tag == "Enemy Unit")
                    {
                        return BU;

                    }
                    else if (BU.transform.tag == "Enemy Unit" && transform.tag == "Player Unit")
                    {
                        return BU;
                    }
                } catch { }
            }

            return null; 
        }

        public override void Clicked()
        {
            base.Clicked();
            if (!Enemy)
            {
                DragSelection drag = FindObjectOfType<DragSelection>();
                drag.Deselect();
                drag.Selected_Pieces.Add(this);
                Debug.Log("Sir!");
            }
        }


        public void Attack(BattleUnit unit)
        {
            transform.LookAt(unit.transform);
            fired = true;
            //Debug.Log("Attack" + unit.name);

            float Attack = BaseStats.Squads[0].Soldiers[0].attack;
            float Defence = unit.BaseStats.Squads[0].Soldiers[0].attack;
            int Dice = Mathf.RoundToInt(Attack - Defence) + 1;
            int Damage = 0;
            //Basic Damage calc 

            for (int i = 0;  i <= Dice; i++)
            {
                Damage += Random.Range(0, 3); // roll based on squad stats in accuracy/luvk etc
            }

            unit.TakeDamage(Damage); 

            //Debug.Log(Dice + " rolls" ); 
            //Debug.Log(Damage + " damage" );
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.parent.transform.position, BaseStats.Squads[0].Soldiers[0].range);
        }
    }
}