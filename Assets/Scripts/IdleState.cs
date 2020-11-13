using UnityEngine;

namespace NWW
{
    public sealed class IdleState : CharacterState
    {
        private void OnEnable()
        {
            Character.Animator.CrossFade("Base Layer.Idle", 0.25f);
        }
    }
}