using Animancer.FSM;
using MenteBacata.ScivoloCharacterController;
using System.Collections;
using UnityEngine;

namespace NWW
{
    public sealed class Character : MonoBehaviour
    {
        public bool lockCursor = false;

        [Header("Character Sheet")]
        [Header("Grounded Movement")]
        [SerializeField] private float moveSpeed = 2;
        [SerializeField] private float runSpeed = 5;
        [SerializeField] private float turnSpeed = 360;
        [SerializeField] private float acceleration = 4;
        [SerializeField] private float jumpSpeed = 10;



        [Header("Airborne Movement")]
        [SerializeField] private float airSpeed = 2;
        [SerializeField] private float airTurnSpeed = 200;
        [SerializeField] private float airAcceleration = 4;
        [SerializeField] private float gravitySpeed = -5f;

        [Header("Crouched Movement")]
        public float crouchMoveSpeed = 1.5f;
        [SerializeField] private float crouchTurnSpeed = 360;
        [SerializeField] private float crouchAcceleration = 4;


        [Header("Required Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterState baseState;
        [SerializeField] private CharacterController controller;
        

        [Header("Optional Components")]
        public MeshRenderer GroundedIndicator;

        public float MoveSpeed => moveSpeed;
        public float RunSpeed => runSpeed;
        public float TurnSpeed => turnSpeed;
        public float Acceleration => acceleration;
        public float JumpSpeed => jumpSpeed;
        public float AirSpeed => airSpeed;
        public float AirTurnSpeed => airTurnSpeed;
        public float AirAcceleration => airAcceleration;
        public float GravitySpeed => gravitySpeed;
        public float CrouchSpeed => crouchMoveSpeed;
        public float CrouchTurnSpeed => crouchTurnSpeed;
        public float CrouchAcceleration => crouchAcceleration;

        public Animator Animator => animator;
        public CharacterController Controller => controller;
        

        public StateMachine<CharacterState> StateMachine { get; private set; }

        private float currentMoveSpeed = 0;

        private void Awake()
        {
            StateMachine = new StateMachine<CharacterState>();
            if (lockCursor)
                Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            StateMachine.TrySetState(baseState);             
        }

        public float GetMoveSpeed()
        {
            float targetSpeed = controller.IsRunning ? runSpeed : moveSpeed;
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, targetSpeed, acceleration * Time.deltaTime);
            return currentMoveSpeed;
        }

        public float GetCrouchSpeed()
        {
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, crouchMoveSpeed, acceleration * Time.deltaTime);
            return currentMoveSpeed;
        }

    }
}