using UnityEngine;

namespace Project4X2
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Building")]

    public class Building : ScriptableObject
    {
        public Sprite Sprite; 
        public int tier, turns_to_build;
        //List<Unit> Unlockable Units
        //Building Prerquisit

    }
}