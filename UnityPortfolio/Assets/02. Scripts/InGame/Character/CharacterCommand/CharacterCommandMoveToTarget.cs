using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class CharacterCommandMoveToTarget : CharacterCommand
    {
        public CharacterCommandMoveToTarget(PlayerCharacter character) : base(character)
        {

        }

        public override void OnStart()
        {
            m_character.MoveToTarget();
        }

        public override void OnUpdate()
        {
            if(Vector3.Distance(m_character.transform.position, m_character.target.transform.position)
                < 0.3f)
            {
                m_isEnd = true;
            }
        }

        public override void OnEnd()
        {
            m_character.MoveEnd();
        }
    }
}