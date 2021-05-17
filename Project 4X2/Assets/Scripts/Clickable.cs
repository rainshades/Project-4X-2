using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Project4X2
{
    public class Clickable : MonoBehaviour
    {
        protected virtual void Clicked()
        {

        }

        private void OnMouseDown()
        {
            Debug.Log(name + " Clicked");
            Clicked();
        }

    }
}