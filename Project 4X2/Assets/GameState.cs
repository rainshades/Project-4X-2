using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Pathfinding; 

namespace Project4X2
{

    [System.Serializable]
    public struct SerializableVector3
    {
        /// <summary>
        /// x component
        /// </summary>
        public float x;

        /// <summary>
        /// y component
        /// </summary>
        public float y;

        /// <summary>
        /// z component
        /// </summary>
        public float z;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="rZ"></param>
        public SerializableVector3(float rX, float rY, float rZ)
        {
            x = rX;
            y = rY;
            z = rZ;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}]", x, y, z);
        }

        /// <summary>
        /// Automatic conversion from SerializableVector3 to Vector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator Vector3(SerializableVector3 rValue)
        {
            return new Vector3(rValue.x, rValue.y, rValue.z);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to SerializableVector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator SerializableVector3(Vector3 rValue)
        {
            return new SerializableVector3(rValue.x, rValue.y, rValue.z);
        }
    }

    public class GameState : MonoBehaviour
    {
        [System.Serializable]
        struct V3
        {
            public float x, y, z;

            public V3(float x, float y, float z)
            {
                this.x = x; this.y = y; this.z = z;
            }

            public V3(Vector3 vector3)
            {
                x = vector3.x; y = vector3.y; z = vector3.z;
            }

        }

        public static GameState Instance; 
        [System.Serializable]
        private class GameData
        {
            public List<SerializableVector3> ArmyLocations = new List<SerializableVector3>();
            public List<Army> ArmiesInGame = new List<Army>();
            public List<SettlementInfo> AllTheSettlementInfo = new List<SettlementInfo>();
        } //Dont' forget to update with all the data needed

        GameData MapData = new GameData(); 

        private void Awake()
        {
            Instance = this;
            
            if (FindObjectsOfType<GameState>().Length > 1)
            {
                Destroy(FindObjectsOfType<GameState>()[1].gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void GetEndTurnData()
        {
            LocationData();
            ArmyData();
            SettlementData(); 
        }

        void LocationData()
        {
            if(MapData.ArmyLocations != null)
                MapData.ArmyLocations.Clear();
            
            foreach (AIPath path in FindObjectsOfType<AIPath>())
            {
                MapData.ArmyLocations.Add(path.transform.position);
            }
        }

        void ArmyData()
        {
            if (MapData.ArmiesInGame != null)
                MapData.ArmiesInGame.Clear();
            
            foreach (AttatchedArmy path in FindObjectsOfType<AttatchedArmy>())
            {
                MapData.ArmiesInGame.Add(path.Army);
            }
        }

        void SettlementData()
        {
            if (MapData.AllTheSettlementInfo != null)
                MapData.AllTheSettlementInfo.Clear(); 

            foreach(Settlement settlement in FindObjectsOfType<Settlement>())
            {
                settlement.ThisSettlement.SyncBuilding(settlement.BuildingSlots); 
                MapData.AllTheSettlementInfo.Add(settlement.ThisSettlement);
            }
        }

        public void Save(string filepath)
        {
            GetEndTurnData(); 

            try
            {
                string autosave = Application.persistentDataPath + "/" + filepath + ".json";
                FileStream file = File.Create(autosave);
                string json = JsonUtility.ToJson(MapData);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, json);
                file.Close();
            }
            catch { Debug.LogError("Error Saving"); }
        }

        public void Load(string filepath)
        {
            try
            {
                string autosave = Application.persistentDataPath + "/" + filepath + ".json";
                FileStream file = File.Open(autosave, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                string json = (string)bf.Deserialize(file);
                MapData = JsonUtility.FromJson<GameData>(json);
                file.Close();
            }
            catch { Debug.LogError("Error Loading"); }

            int iterator = 0; 
            foreach(AIPath piece in FindObjectsOfType<AIPath>())
            {
                piece.transform.position = MapData.ArmyLocations[iterator];
                iterator++;
            }

        }

        public void AutoSave()
        {
            Save("autosave");
        }

        public void LoadAudoSave()
        {
            Load("autosave");
        }


    }
}