using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public abstract class SkillBase
    {
        public enum State
        {
            StandBy,
            Execute,
            CoolTime
        }

        protected SkillData m_skillData;
        protected float m_coolTimer;
        protected State m_state;
        protected PlayerCharacter m_character;

        public SkillBase(PlayerCharacter character, SkillData data)
        {
            m_character = character;
            m_skillData = data;
            m_state = State.StandBy;
        }
        public abstract void Update();
        public abstract void Play();
    }

    public class DefaultSkill : SkillBase
    {
        public DefaultSkill(PlayerCharacter character, SkillData data)
            : base(character, data) { }

        public override void Update()
        {
        }

        public override void Play()
        {
            m_character.PlaySkill(m_skillData);
            m_state = State.Execute;
        }
    }
}
