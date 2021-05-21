using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public abstract class SkillBase
    {
        enum State
        {
            StandBy,
            Execute,
            CoolTime
        }

        SkillData m_skillData;
        float m_coolTimer;
        State m_state;
        //virtual void Func() = 0;
        public abstract void Update();
        public abstract void Play();
    }
}
