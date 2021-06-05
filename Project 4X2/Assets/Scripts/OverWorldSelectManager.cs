///Credit for the Raycasting Code
///https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#ray_to_terrain
///
using System.Collections; 
using UnityEngine;
using UnityEngine.InputSystem;
using Pathfinding; 

namespace Project4X2
{
    public class OverWorldSelectManager : MonoBehaviour
    {
        public static OverWorldSelectManager Instance;

        public AudioSource UnitVoiceLines { get => GetComponentInChildren<AudioSource>(); }
        public Clickable CurrentSelection;
        public LayerMask PlayerMask;

        public LayerMask EnemyMasks;
        public LayerMask MovementRangeMask; 
        public LayerMask SettlementLayers; 
        public Clickable Enemy; 

        TerrainCollider terrain;
        Vector3 worldPosition;
        Ray ray;

        public Sprite DestinationMarker;
        GameObject go;

        [SerializeField]
        GameObject ArmyPrefab;

        public LayerMask ClickableMasks; 

        private void Awake()
        {
            Instance = this; 
        }

        private void Start()
        {
            terrain = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        }

        private void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                try
                {
                    OverworldUnit OW = CurrentSelection as OverworldUnit;
                    OW.Movement.TurnOffNotifier();
                }
                catch { Debug.Log("No Current Selected"); }

                if (Physics.Raycast(ray, out hitData, 10000, ClickableMasks)){

                    try
                    {
                        CurrentSelection = hitData.transform.gameObject.GetComponent<Clickable>();
                        CurrentSelection.Clicked();
                        Debug.Log("Clicked");
                    } catch { }
                }
                else
                {

                    CurrentSelection = null;
                    OverWorldUIController.Instance.HideDeck();
                }
            }

            if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                if(terrain.Raycast(ray, out hitData, 10000))
                {
                    if (CurrentSelection is OverworldUnit)
                    {
                        OverworldUnit OW = CurrentSelection as OverworldUnit;
                        OW.Movement.TurnOnNotifier();
                        go = new GameObject();
                        go.AddComponent<SpriteRenderer>().sprite = DestinationMarker;

                        go.SetActive(true);
                        worldPosition = hitData.point;
                        go.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
                        go.transform.eulerAngles = new Vector3(90, 0, 90);
                        StartCoroutine(DestroyObject(go));

                        if ((go.transform.position - OW.transform.position).magnitude < OW.Movement.movement_range)
                        {
                            OW.Moving = true;
                            
                            UnitVoiceLines.clip = OW.MoveingAudio;

                            UnitVoiceLines.Play();

                            OW.GetComponentInParent<AIPath>().destination = worldPosition;


                            OW.Ani.SetTrigger("Moving");

                        }

                    }

                    if(CurrentSelection is Settlement)
                    {
                        Settlement OS = CurrentSelection as Settlement; 
                        if(OverWorldUIController.Instance.GetComponentInChildren<ArmyUI>().SelectedCards.Count > 0)
                        {
                            GameObject fab = Instantiate(ArmyPrefab);
                            fab.transform.position = OS.SpawnPoint.position;
                            fab.GetComponent<Army>().AssignTag(OS.Owner == FactionManager.instance.PlayerFaction);
                            foreach(GameObject unit in OverWorldUIController.Instance.GetComponentInChildren<ArmyUI>().SelectedCards)
                            {
                                Unit selectedUnit = unit.GetComponent<UnitCard>().recruit; 
                                fab.GetComponent<Army>().Units.Add(selectedUnit);
                                OS.GetComponent<Army>().Disband(selectedUnit);
                            }

                            OverWorldUIController.Instance.GetComponentInChildren<ArmyUI>().RemoveArmy();
                            CurrentSelection = fab.GetComponentInChildren<OverworldUnit>();

                            OverworldUnit OW = CurrentSelection as OverworldUnit;
                            OW.Clicked();

                            go = new GameObject();
                            worldPosition = hitData.point;
                            go.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
                            go.transform.eulerAngles = new Vector3(90, 0, 90);
                            OW.Destination.target = go.transform;
                        }
                    }
                }

                if(Physics.Raycast(ray, out hitData, 1000, EnemyMasks))
                {
                    //if Enemy Unit
                    OverworldUnit OW = CurrentSelection as OverworldUnit;

                    OW.MakeAttackMove(hitData.transform.GetComponent<Clickable>());
                    OW.Destination.target = hitData.transform;
                } 

                if(Physics.Raycast(ray, out hitData, 1000, SettlementLayers))
                {
                    OverworldUnit OW = CurrentSelection as OverworldUnit;
                    Settlement settlement = hitData.transform.GetComponent<Settlement>();
                    
                    if(settlement.tag == "Player")
                    {
                        OW.MakeCombineMove(settlement);
                    }
                    else if(settlement.tag == "NPF")
                    {
                        OW.MakeAttackMove(settlement);
                    }
                }

            }

            //Where RightClick on Pathfindable Terrain
            //CurrentSelection.Destination

        }
        IEnumerator DestroyObject(GameObject gameObject)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Destroy(gameObject);
        }
    }
}