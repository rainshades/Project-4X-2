using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class Attack : MonoBehaviour
    {
        public ParticleSystem PS; 

        public void AttackParticlesOn()
        {
            PS.gameObject.SetActive(true);
        }


        public void AttackParticlesOff()
        {
            PS.gameObject.SetActive(false);
        }
    }
}