using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; 

namespace Project4X2
{
    public class BattleUnitAnimationController : MonoBehaviour
    {
        AIPath Pathfind; 
        Animator[] Ani;

        private void Awake()
        {
            Pathfind = GetComponentInParent<AIPath>();
            Ani = GetComponentsInChildren<Animator>();
        }

        public void WalkingAnimation()
        {
            foreach(Animator ani in Ani)
            {
                ani.SetTrigger("Walking");
            }
        }

        public void Idle()
        {
            foreach(Animator ani in Ani)
            {
                ani.SetTrigger("Idle");
            }
        }

        public void Fire()
        {
            foreach(Animator ani in Ani)
            {
                ani.SetTrigger("Attack");
            }
        }

    }
}