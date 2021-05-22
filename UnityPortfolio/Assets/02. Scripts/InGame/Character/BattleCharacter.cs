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

        protected List<SkillBase> m_skillList = new List<SkillBase>();

        public int hp { get { return m_hp; } }
        public int fullHP { get { return m_fullHP; } }

        protected override void Update()
        {
            base.Update();

            for(int i =0; i < m_skillList.Count; ++i)
            {
                m_skillList[i].Update();
            }
        }

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

        public virtual void AddSkill(SkillBase skill)
        {
            m_skillList.Add(skill);
        }
    }
}