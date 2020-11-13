using UnityEngine;

namespace NWW
{
    public sealed class AmbulationState : CharacterState
    {
        private void OnEnable()
        {
            Character.Animator.CrossFade("Base Layer.Ambulation", 0.25f);
        }

        private void Update()
        {
            Vector3 moveDirection = Character.Controller.MoveDirection;
            float moveSpeed = Character.GetMoveSpeed();
            float turnSpeed = Character.GetTurnSpeed();
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection);


            Character.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            Character.transform.rotation = Quaternion.RotateTowards(Character.transform.rotation,
                rotationToMoveDirection, turnSpeed * Time.deltaTime);

            Character.Animator.SetFloat("MoveSpeed", moveSpeed / Character.GetMaxSpeed());

        }
    }
}