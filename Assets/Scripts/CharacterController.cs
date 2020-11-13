using UnityEngine;

namespace NWW
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private Character character;

        [Header("States")]
        [SerializeField] private CharacterState idleState;
        [SerializeField] private CharacterState ambulationState;

        public bool IsRunning { get; private set; }
        public Vector3 MoveDirection { get; private set; }

        private float horizontal;
        private float vertical;
        private Transform cameraTransform;

        private void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            MoveDirection = Vector3.forward * vertical + Vector3.right * horizontal;
            if (MoveDirection.magnitude < 0.1f)
            {
                character.StateMachine.TrySetState(idleState);
                MoveDirection = Vector3.zero;
                return;
            }            
            
            Vector3 projectedCameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectedCameraForward);

            MoveDirection = rotationToCamera * MoveDirection;
            character.StateMachine.TrySetState(ambulationState);
        }


        public void OnMoveInput(float horizontal, float vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }

        public void OnRunInput(bool runInput)
        {
            IsRunning = runInput;
        }

    }
}