using UnityEngine;
using MenteBacata.ScivoloCharacterController;

namespace NWW
{
    public sealed class AmbulationState : CharacterState
    {
        [SerializeField] private CharacterMover characterMover;
        private float gravitySpeed = 5f;

        private void OnEnable()
        {
            Character.Animator.CrossFade("Base Layer.Ambulation", 0.25f);
        }

        private void Update()
        {
            Vector3 moveDirection = Character.Controller.MoveDirection;
            float moveSpeed = Character.GetMoveSpeed();
            float turnSpeed = Character.GetTurnSpeed();
           
            Vector3 moveVelocity = moveDirection * moveSpeed;            
            moveVelocity.y = Mathf.Min(moveVelocity.y, -gravitySpeed);
                        
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection);                       
            Character.transform.rotation = Quaternion.RotateTowards(Character.transform.rotation,
                rotationToMoveDirection, turnSpeed * Time.deltaTime);

            //Character.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            characterMover.Move(moveVelocity * Time.deltaTime);
            
            Character.Animator.SetFloat("MoveSpeed", moveSpeed / Character.GetMaxSpeed());

            

        }
    }
}