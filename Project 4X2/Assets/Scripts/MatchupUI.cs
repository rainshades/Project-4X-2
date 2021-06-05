using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project4X2
{
    public class MatchupUI : MonoBehaviour
    {
        public GameObject PlayerSide, EnemySide;

        public static MatchupUI instance;
        private void Awake()
        {
            instance = this; 
        }

    }
}