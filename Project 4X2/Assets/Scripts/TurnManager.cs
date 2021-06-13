using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

namespace Project4X2
{
    public enum Season { Winter, Heavy_Winter, Spring, Summer, Heavy_Summer, Fall, Rainy }
    
    [System.Serializable]
    public class TurnInfo
    {
        public Season CurrentSeason = Season.Winter;
        public int TurnNumber = 0;
    }

    public class TurnManager : MonoBehaviour
    {
        [SerializeField]TextMeshProUGUI NumberText, SeasonText, BankText;
        public TurnInfo Turninfo = new TurnInfo(); 

        public Season CurrentSeason { get => Turninfo.CurrentSeason; set => Turninfo.CurrentSeason = value;  }
        public int TurnNumber { get => Turninfo.TurnNumber; set => Turninfo.TurnNumber = value;  }

        public static TurnManager instance;

        private void Awake()
        {
            instance = this; 
        }

        private void Start()
        {
            NumberText.text = TurnNumber + "";
            SeasonText.text = CurrentSeason.ToString();
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