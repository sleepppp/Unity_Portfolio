using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class CharacterCommandMoveTo : ICharacterCommand
    {
        Vector3 m_arrival;
        Character m_character;
        float m_endDistance;

        public CharacterCommandMoveTo(Character target, Vector3 arrival,float endDistance)
        {
            m_character = target;
            m_endDistance = endDistance;
            m_arrival = arrival;
        }

        public bool IsEnd()
        {
            return Vector3.Distance(m_character.transform.position, m_arrival) <= m_endDistance;
        }

        public void OnEnd()
        {
            m_character.MoveEnd();
        }

        public void OnStart()
        {
            m_character.MoveTo(m_arrival);
        }

        public void OnUpdate()
        {

        }
    }
}
