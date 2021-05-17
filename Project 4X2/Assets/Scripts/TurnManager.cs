using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class TurnManager : MonoBehaviour
    {
        public Clickable CurrentUnit;
        public int TurnNumber = 0; 

        [SerializeField]
        Clickable[] Units;


        private void Start()
        {
            Units = GetComponentsInChildren<Clickable>();
            CurrentUnit = Units[TurnNumber];
        }

        public void NextTurn()
        {
            TurnNumber++;
            if(TurnNumber < Units.Length)
            {
                CurrentUnit = Units[TurnNumber];
            }
            else
            {
                CurrentUnit = Units[0];
            }
        }
    }
}