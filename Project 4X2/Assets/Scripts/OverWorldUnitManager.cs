///Credit for the Raycasting Code
///https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#ray_to_terrain
///

using UnityEngine;
using UnityEngine.InputSystem;

namespace Project4X2
{
    public class OverWorldUnitManager : MonoBehaviour
    {
        public static OverWorldUnitManager Instance { get => FindObjectOfType<OverWorldUnitManager>(); }

        public AudioSource UnitVoiceLines { get => GetComponentInChildren<AudioSource>(); }
        public Overworld CurrentSelection;
        public LayerMask EnemyMasks;

        public Clickable Enemy; 

        TerrainCollider terrain;
        Vector3 worldPosition;
        Ray ray;

        public Sprite DestinationMarker;
        [SerializeField]
        GameObject go; 

        private void Start()
        {
            terrain = Terrain.activeTerrain.GetComponent<TerrainCollider>();
            go = new GameObject();
            go.AddComponent<SpriteRenderer>().sprite = DestinationMarker;
        }

        private void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData; 

            if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                if(terrain.Raycast(ray, out hitData, 10000))
                {
                    UnitVoiceLines.clip = CurrentSelection.MoveingAudio; 
                    UnitVoiceLines.Play();
                    
                    go.SetActive(true);
                    worldPosition = hitData.point;
                    go.transform.position = new Vector3 (worldPosition.x, 0.5f, worldPosition.z);
                    go.transform.eulerAngles = new Vector3(90, 0, 90);
                    CurrentSelection.Destination.target = go.transform;

                    foreach (Animator ani in CurrentSelection.Animators)
                    {
                        ani.SetTrigger("Moving");
                    }
                }

                if(Physics.Raycast(ray, out hitData, 1000, EnemyMasks))
                {
                    //if Enemy Unit
                    CurrentSelection.MakeAttackMove(hitData.transform.GetComponent<Clickable>());
                    CurrentSelection.Destination.target = hitData.transform;

                } 

            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
            }

            //Where RightClick on Pathfindable Terrain
            //CurrentSelection.Destination
        }
    }
}