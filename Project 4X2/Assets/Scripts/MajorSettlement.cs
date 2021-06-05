using UnityEngine;
using System.Collections.Generic;

namespace Project4X2
{
    public partial class MajorSettlement : Settlement
    {
        private void Awake()
        {
            BuildingSlots = new BuildingSlot[5];

        }
    }
}