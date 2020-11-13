using Animancer.FSM;
using UnityEngine;

namespace NWW
{
    public abstract class CharacterState : StateBehaviour<CharacterState>
    {
        [SerializeField] private Character character;
        public Character Character { get => character; }
    }
}