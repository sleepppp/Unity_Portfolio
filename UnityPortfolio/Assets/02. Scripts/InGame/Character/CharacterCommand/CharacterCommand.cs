using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public abstract class CharacterCommand
    {
        protected PlayerCharacter m_character;
        protected bool m_isEnd;

        public CharacterCommand(PlayerCharacter character)
        {
            m_character = character;
            m_isEnd = false;
        }

        public abstract void OnStart();
        public abstract void OnUpdate();
        public abstract void OnEnd();
    }
}

