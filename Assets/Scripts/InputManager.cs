using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[System.Serializable]
public class InputEventVector2 : UnityEvent<float, float> { }

[System.Serializable]
public class InputEventBool : UnityEvent<bool> { }


namespace NWW
{
    public sealed class InputManager : MonoBehaviour
    {
        private Controls controls;
        public InputEventBool crouchInputEvent;
        public InputEventVector2 moveInputEvent;
        public InputEventBool runInputEvent;

        private void Awake()
        {
            controls = new Controls();
        }

        private void OnEnable()
        {
            controls.Gameplay.Enable();
            controls.Gameplay.Move.performed += OnMove;
            controls.Gameplay.Move.canceled += OnMove;
            controls.Gameplay.Run.performed += OnRun;
            controls.Gameplay.Run.canceled += OnRun;
            controls.Gameplay.Crouch.performed += OnCrouch;
            //controls.Gameplay.Crouch.canceled += OnCrouch;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            moveInputEvent.Invoke(moveInput.x, moveInput.y);
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            bool runInput = context.ReadValueAsButton();
            runInputEvent.Invoke(runInput);
        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            bool crouchInput = context.ReadValueAsButton();
            crouchInputEvent.Invoke(crouchInput);
        }
    }
}