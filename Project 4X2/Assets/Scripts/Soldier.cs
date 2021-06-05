using UnityEngine;

namespace Project4X2
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Soldiers")]
    public class Soldier : ScriptableObject
    {
        public enum SoldierType { light_infintry, heavy_infintry, special_infintry, sniper_team, light_vehicle, heavy_vehicle, custom_armor, heavy_armor, other}
        public SoldierType Type; 
        public float ammo, health, attack, defence, speed, range, armorpen, reload_Speed; 
    }
}