using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project4X2
{
    public class CameraPoint : MonoBehaviour
    {
        CamActions CameraMovement;

        public float CameraMoveSpeed;
        CharacterController cc; 

        private void Awake()
        {
            cc = GetComponent<CharacterController>();
            CameraMovement = new CamActions();
            CameraMovement.CameraMovement.Movement.performed += Movement_performed;
            CameraMovement.CameraMovement.Scroll.performed += Scroll_performed;
        }

        private void Scroll_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Vector3 MovementVector = new Vector3 (0, obj.ReadValue<float>() / 2, 0);
            cc.Move(MovementVector);
        }

        private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Vector3 MovementVector = new Vector3 (obj.ReadValue<Vector2>().x, 0, obj.ReadValue<Vector2>().y);
            cc.Move(MovementVector * -CameraMoveSpeed);
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