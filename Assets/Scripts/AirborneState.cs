using UnityEngine;

namespace NWW
{
    public sealed class AirborneState : CharacterState
    {
        private void OnEnable()
        {
            if (Character.GroundedIndicator != null)
                Character.GroundedIndicator.material.color = Color.cyan;

            Character.Animator.CrossFade("Base Layer.Falling", 0.25f);
        }

        private void Update()
        {
            Vector3 moveDirection = Character.Controller.MoveDirection;                       
            
            float moveSpeed = Character.AirSpeed;
            float turnSpeed = Character.AirTurnSpeed;

            Vector3 moveVelocity = moveDirection * moveSpeed;
            moveVelocity.y = Character.GravitySpeed;

            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection);
            Character.transform.rotation = Quaternion.RotateTowards(Character.transform.rotation,
                rotationToMoveDirection, turnSpeed * Time.deltaTime);

            Character.Controller.Mover.Move(moveVelocity * Time.deltaTime);
        }
    }
}