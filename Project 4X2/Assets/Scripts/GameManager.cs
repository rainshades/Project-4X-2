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

        public void NewGame()
        {
            SceneManager.LoadScene(0);
            SceneManager.sceneLoaded += LoadNewGame;

        }

        private void ContinueGame(Scene arg0, LoadSceneMode arg1)
        {
            GameState.Instance.LoadAudoSave();
            SceneManager.sceneLoaded -= ContinueGame;
        }

        private void LoadNewGame(Scene scene, LoadSceneMode mode)
        {
            foreach(Faction faction in FactionManager.instance.Factions)
            {
                faction.NewGame(); 
            }
            SceneManager.sceneLoaded -= LoadNewGame; 
        }

        public void LoadBattleScene()
        {
            GameState.Instance.AutoSave();
            SceneManager.LoadScene(BattleScene);
        }


        public void LoadOverworldScene()
        {
            SceneManager.LoadScene(0);
            SceneManager.sceneLoaded += ContinueGame;
        }
    }
}