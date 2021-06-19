using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Project4X2
{
    public class OverworldGameUI : MonoBehaviour
    {
        public void ExitToStartMenu()
        {
            SceneManager.LoadScene(2);
        }
        public void ExitToDesktop()
        {
            Application.Quit();
        }
    }
}