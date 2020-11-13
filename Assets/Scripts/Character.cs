using Animancer.FSM;
using UnityEngine;

namespace NWW
{
    public sealed class Character : MonoBehaviour
    {
        [Header("Character Sheet")]
        [SerializeField] private float moveSpeed = 2;
        [SerializeField] private float runSpeed = 5;
        [SerializeField] private float turnSpeed = 360;


        [Header("Required Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterState idleState;
        [SerializeField] private CharacterController controller;

        public Animator Animator { get => animator; }
        public CharacterController Controller { get => controller; }

        public StateMachine<CharacterState> StateMachine { get; private set; }

        private void Awake()
        {
            StateMachine = new StateMachine<CharacterState>();
        }

        private void Start()
        {
            StateMachine.TrySetState(idleState);
        }

        public float GetMoveSpeed()
        {
            return controller.IsRunning ? runSpeed : moveSpeed;
        }

        public float GetTurnSpeed()
        {
            return turnSpeed;
        }

        public float GetMaxSpeed()
        { 
            return runSpeed;
        }
    }
}