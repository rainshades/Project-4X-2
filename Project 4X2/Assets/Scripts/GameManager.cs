using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Project4X2
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        string BattleScene; //Variation battle scene based on location

        public static GameManager Instance { get => FindObjectOfType<GameManager>(); }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }


        public void LoadBattleScene()
        {
             SceneManager.LoadScene(BattleScene);
        }

    }
}