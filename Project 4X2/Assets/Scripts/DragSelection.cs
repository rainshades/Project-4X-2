////Inspiration and Selection Code credit goes to 
////UnityCodeMonkey.com 
///https://unitycodemonkey.com/downloadpage.php?yid=y601TRfoxc4
///



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Pathfinding; 

namespace Project4X2
{
    public class DragSelection : MonoBehaviour
    {
        Vector3 worldPosition;
        Vector3 SelectStartPosition;
        Ray ray;

        [SerializeField]
        Transform SelectedAreaTransform;
        public List<BattleUnit> Selected_Pieces;
        RaycastHit hitData;

        [SerializeField]
        LayerMask DragCollision = new LayerMask();

        TerrainCollider terrain;
        GameObject go;
        [SerializeField] Sprite MoveToSprite; 

        void Start()
        {
            SelectedAreaTransform.gameObject.SetActive(false);
            terrain = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        }

        void Update()
        {

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Deselect();
                SelectStartPosition = MouseToTerrainPos();
                SelectedAreaTransform.gameObject.SetActive(true);
            }

            if (Mouse.current.leftButton.isPressed)
            {
                CalculateSelectionLowerLeftUpperRight(out Vector3 lowerLeft, out Vector3 upperRight);
                SelectedAreaTransform.position = lowerLeft;
                SelectedAreaTransform.localScale = upperRight - lowerLeft;
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                SelectedAreaTransform.gameObject.SetActive(false);

                CalculateSelectionLowerLeftUpperRight(out Vector3 lowerLeft, out Vector3 upperRight);

                // Calculate Center and Extents
                Vector3 selectionCenterPosition = new Vector3(
                    lowerLeft.x + ((upperRight.x - lowerLeft.x) / 2f),
                    0,
                    lowerLeft.z + ((upperRight.z - lowerLeft.z) / 2f)
                );

                Vector3 halfExtents = new Vector3(
                    (upperRight.x - lowerLeft.x) * .5f,
                    1,
                    (upperRight.z - lowerLeft.z) * .5f
                );

                // Set min size
                float minSelectionSize = .5f;
                if (halfExtents.x < minSelectionSize) halfExtents.x = minSelectionSize;
                if (halfExtents.z < minSelectionSize) halfExtents.z = minSelectionSize;

                // Find Objects within Selection Area
                Collider[] colliderArray = Physics.OverlapBox(selectionCenterPosition, halfExtents,
                    Quaternion.identity, DragCollision);

                foreach (Collider collider in colliderArray)
                {
                    BattleUnit BU = collider.GetComponentInParent<BattleUnit>();
                    try
                    {
                        if (!BU.IsEnemy() && !Selected_Pieces.Contains(BU))
                        {
                            BU.SetIsSelected(true);
                            Selected_Pieces.Add(BU);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            if(Mouse.current.rightButton.wasPressedThisFrame && Selected_Pieces.Count > 0)
            {
                int UnitPositionBuffer = -5; 
                foreach (BattleUnit unit in Selected_Pieces)
                {
                    go = new GameObject();
                    worldPosition = MouseToTerrainPos();
                    go.transform.position = new Vector3(worldPosition.x + UnitPositionBuffer
                        , 0.5f, worldPosition.z);
                    go.transform.eulerAngles = new Vector3(90, 0, 90);
                    StartCoroutine(DestroyDestination(go));

                    if (unit.Battle_Started)
                    {
                        unit.GetComponentInParent<AIPath>().destination = go.transform.position; 
                    }
                    else
                        unit.transform.parent.position = go.transform.position;

                    UnitPositionBuffer += 5; 
                }
            }
        }


        IEnumerator DestroyDestination(GameObject go )
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Destroy(go);
        }

        public void Deselect()
        {
            Selected_Pieces.Clear();
        }

        private void CalculateSelectionLowerLeftUpperRight(out Vector3 lowerLeft, out Vector3 upperRight)
        {
            Vector3 currentMousePosition = MouseToTerrainPos();
            lowerLeft = new Vector3(
                Mathf.Min(SelectStartPosition.x, currentMousePosition.x),
                0.5f,
                Mathf.Min(SelectStartPosition.z, currentMousePosition.z)
            );
            upperRight = new Vector3(
                Mathf.Max(SelectStartPosition.x, currentMousePosition.x),
                0.5f,
                Mathf.Max(SelectStartPosition.z, currentMousePosition.z)
            );
        }


        public Vector3 MouseToTerrainPos()
        {
            Ray hit = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (terrain.Raycast(hit, out hitData, 10000))
            {
                return hitData.point;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}
