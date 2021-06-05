using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class CameraPoint : MonoBehaviour
    {
        CamActions CameraMovement;

        [Range(-25, 25)]
        public float CameraMoveSpeed;
        CharacterController cc;
        Vector3 MovementVector; 

        private void Awake()
        {
            cc = GetComponent<CharacterController>();
            CameraMovement = new CamActions();
            CameraMovement.CameraMovement.Movement.performed += Movement_performed;
            CameraMovement.CameraMovement.Scroll.performed += Scroll_performed;

            CameraMovement.CameraMovement.Movement.canceled += ctx => MovementVector = Vector3.zero;
            CameraMovement.CameraMovement.Scroll.canceled += ctx => MovementVector = Vector3.zero;
        }

        private void Update()
        {
            cc.Move(MovementVector * -CameraMoveSpeed * Time.deltaTime);
        }


        private void Scroll_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            MovementVector = new Vector3 (0, obj.ReadValue<float>() / 2, 0);
        }

        private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            MovementVector = new Vector3 (obj.ReadValue<Vector2>().x, 0, obj.ReadValue<Vector2>().y);
        }

        private void OnEnable()
        {
            CameraMovement.Enable();
        }
        private void OnDisable()
        {
            CameraMovement.Disable();
        }
    }
}