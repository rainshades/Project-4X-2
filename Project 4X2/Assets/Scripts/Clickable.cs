using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Project4X2
{
    public class Clickable : MonoBehaviour
    {
        public bool Selected;
        public void SetIsSelected(bool value) { Selected = value; }

        public virtual void Clicked()
        {

        }

        private void OnMouseDown()
        {
            try
            {
                OverworldUnit OW = OverWorldSelectManager.Instance.CurrentSelection as OverworldUnit;
                OW.Movement.TurnOffNotifier();
            }
            catch { Debug.Log("No Current Selected"); }

            Clicked();

        }
    }
}