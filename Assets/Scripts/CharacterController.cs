using UnityEngine;
using MenteBacata.ScivoloCharacterController;

namespace NWW
{

    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private GroundDetector groundDetector;
        [SerializeField] private CharacterMover mover;

        [Header("States")]
        [SerializeField] private CharacterState ambulationState;
        [SerializeField] private CharacterState airborneState;
        [SerializeField] private CharacterState crouchedState;

        public GroundDetector GroundDetector => groundDetector;
        public CharacterMover Mover => mover;
        
        public bool IsMoving { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsGrounded { get; private set; }
        public bool IsCrouching { get; private set; }
        public bool IsJumping { get; private set; }


        public Vector3 MoveDirection { get; private set; }
        public float CurrentMoveSpeed { get; private set; }

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
            
            // Adventure Movement Direction
            Vector3 projectedCameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectedCameraForward);            
            MoveDirection = rotationToCamera * MoveDirection;

            IsMoving = MoveDirection.magnitude >= 0.1f;
            IsGrounded = groundDetector.DetectGround(out GroundInfo groundInfo);

            if (!IsGrounded)
            {
                character.StateMachine.TrySetState(airborneState);
                return;
            }

            if (character.GroundedIndicator != null)
                character.GroundedIndicator.material.color = Color.gray;

            if (IsCrouching)
            {
                character.StateMachine.TrySetState(crouchedState);
                return;
            }
            
            //if (MoveDirection.magnitude >= 0.1f)
            //{
            //    character.StateMachine.TrySetState(ambulationState);
            //    return;
            //}

            //MoveDirection = Vector3.zero;
            character.StateMachine.TrySetState(ambulationState);
            Debug.Log($"{CurrentMoveSpeed}");
        }

        public void OnMoveInput(float horizontal, float vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
            Debug.Log($"{horizontal},{vertical}");
        }

        public void OnRunInput(bool runInput)
        {
            IsRunning = runInput;
        }

        public void OnCrouchInput(bool crouchInput)
        {
            IsCrouching = !IsCrouching;
        }

        public void OnJumpInput(bool jumpInput)
        {
            IsJumping = jumpInput;
        }



        public void SetCurrentMoveSpeed(float acceleration, float targetSpeed)
        {
            float moveSpeed = Mathf.MoveTowards(CurrentMoveSpeed, targetSpeed,
                acceleration * Time.deltaTime);
            CurrentMoveSpeed = moveSpeed;
        }

    }
}