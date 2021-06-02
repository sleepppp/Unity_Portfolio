using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCore.Data;
namespace MyCore
{
    public class SkillBase
    {
        protected bool m_isCoolDown;
        protected BattleCharacter m_playerCharacter;
        protected SkillData m_data;
        protected float m_coolTimer;

        public bool isCoolDown { get { return m_isCoolDown; } }
        public float coonTimer { get { return m_coolTimer; } }
        public SkillData skillData { get { return m_data; } }

        public void Init(BattleCharacter character , SkillData data)
        {
            m_data = data;
            m_isCoolDown = false;
            m_playerCharacter = character;
        }

        public void Update()
        {
            if(m_isCoolDown)
            {
                m_coolTimer += Time.deltaTime;

                if(m_coolTimer >= m_data.CoolTime)
                {
                    m_isCoolDown = false;
                }
            }
        }

        public virtual void Play()
        {
            if (m_isCoolDown == true)
                return;
            m_isCoolDown = true;
            m_coolTimer = 0f;
            m_playerCharacter.PlaySkillAnim(m_data.MontageName);
            GameEvent.instance.OnEventPlaySkill(this);
        }
    }

    public class DefaultAttack : SkillBase  
    {

    }
}
