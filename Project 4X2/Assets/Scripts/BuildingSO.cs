using UnityEngine;

namespace Project4X2
{
    [System.Serializable]
    public class Building
    {
        public Sprite Sprite;
        public int tier, turns_to_build;
        public bool enabled = false; 
        //List<Unit> Unlockable Units
        //Building Prerquisit
    }

    [CreateAssetMenu(fileName = "New Building", menuName = "Building")]
    public class BuildingSO : ScriptableObject
    {
        public Building Building; 
    }
}