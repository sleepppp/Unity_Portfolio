using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class BattleCharacter : Character
    {
        [Header("BattleCharacter")]
        [SerializeField]protected int m_fullHP;
        [SerializeField]protected int m_hp;

        public int hp { get { return m_hp; } }
        public int fullHP { get { return m_fullHP; } }

        public virtual void ApplyDamage(int damage)
        {
            if (m_hp <= 0)
                return;

            m_hp -= damage;

            if(m_hp <= 0)
            {
                OnDead();
            }
        }

        public virtual void OnDead()
        {

        }
    }
}