using UnityEngine;

namespace NWW
{
    public sealed class IdleState : CharacterState
    {
        private void OnEnable()
        {
            if (Character.GroundedIndicator != null)
                Character.GroundedIndicator.material.color = Color.gray;

            Character.Animator.CrossFade("Base Layer.Idle", 0.25f);
        }
    }
}