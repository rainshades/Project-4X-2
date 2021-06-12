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
        public static GameManager Instance;

        private void Awake()
        {
            Instance = this; 
            if(FindObjectsOfType<GameManager>().Length > 1)
            {
                Destroy(FindObjectsOfType<GameManager>()[1].gameObject);
            }
            DontDestroyOnLoad(this);
        }

        public void LoadOverworld()
        {
            SceneManager.LoadScene(0);
        }


        public void LoadBattleScene()
        {
            GameState.Instance.AutoSave();
            SceneManager.LoadScene(BattleScene);
        }


        public void LoadOverworldSceneAfterBattle()
        {
            SceneManager.LoadScene(0);
            //Battle casualities
        }
    }
}