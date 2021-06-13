using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class StartMenuUI : MonoBehaviour
    {
        public void StartNewGame()
        {
            GameManager.Instance.NewGame();
        }

        public void ContinueGame()
        {
            GameManager.Instance.LoadOverworldScene();
        }
    }
}