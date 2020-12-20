using UnityEngine;

namespace NWW
{
    public sealed class CrouchedState : CharacterState
    {
        private void OnEnable()
        {
            Character.Animator.CrossFade("Base Layer.Crouching", 0.25f);
        }

        private void Update()
        {
            Vector3 moveDirection = Character.Controller.MoveDirection;
            
            float targetSpeed = Character.Controller.IsMoving ? Character.CrouchSpeed : 0.0f;
            Character.Controller.SetCurrentMoveSpeed(Character.CrouchAcceleration, targetSpeed);
            float moveSpeed = Character.Controller.CurrentMoveSpeed;

            if (Character.Controller.IsMoving)
            {
                //moveSpeed = SetMoveSpeed();
                float turnSpeed = Character.CrouchTurnSpeed;
                float groundClampSpeed = -Mathf.Tan(Mathf.Deg2Rad * Character.Controller.Mover.maxFloorAngle)
                    * moveSpeed;

                Vector3 moveVelocity = moveDirection * moveSpeed;
                moveVelocity.y = groundClampSpeed;

                Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection);
                Character.transform.rotation = Quaternion.RotateTowards(Character.transform.rotation,
                    rotationToMoveDirection, turnSpeed * Time.deltaTime);

                Character.Controller.Mover.Move(moveVelocity * Time.deltaTime);
            }
            Character.Animator.SetFloat("MoveSpeed", moveSpeed / Character.crouchMoveSpeed);
        }
    }
}