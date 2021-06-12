using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

namespace Project4X2
{

    public class TurnManager : MonoBehaviour
    {
        public int TurnNumber = 0;
        [SerializeField]TextMeshProUGUI NumberText, SeasonText, BankText; 

        public enum Season { Winter, Heavy_Winter, Spring, Summer, Heavy_Summer, Fall, Rainy}

        public Season CurrentSeason = Season.Winter;

        private void Start()
        {
            SeasonText.text = "Winter";
            BankText.text = FactionManager.instance.PlayerFaction.bank + "";
        }

        public void NextTurn()
        {   
            FactionManager.instance.PlayerFaction.GainRevenue();
            BankText.text = FactionManager.instance.PlayerFaction.bank + "";
            GameState.Instance.AutoSave(); 
            
            TurnNumber++;
            NumberText.text = "" + TurnNumber;
            switch (CurrentSeason)
            {
                case Season.Winter:
                    CurrentSeason = Season.Heavy_Winter;
                    SeasonText.text = "Winter";
                    break;
                case Season.Heavy_Winter:
                    CurrentSeason = Season.Spring;
                    SeasonText.text = "Spring";
                    break;
                case Season.Spring:
                    CurrentSeason = Season.Summer;
                    SeasonText.text = "Summer";
                    break;
                case Season.Summer:
                    CurrentSeason = Season.Heavy_Summer;
                    SeasonText.text = "Summer";
                    break;
                case Season.Heavy_Summer:
                    CurrentSeason = Season.Fall;
                    SeasonText.text = "Fall";
                    break;
                case Season.Fall:
                    CurrentSeason = Season.Rainy;
                    SeasonText.text = "Fall";
                    break;
                case Season.Rainy:
                    CurrentSeason = Season.Winter;
                    SeasonText.text = "Winter";
                    break;  

            }
        
            foreach(OverWorldMovement OWM in FindObjectsOfType<OverWorldMovement>())
            {
                OWM.resetMovement(); 
            }
        }
    }
}