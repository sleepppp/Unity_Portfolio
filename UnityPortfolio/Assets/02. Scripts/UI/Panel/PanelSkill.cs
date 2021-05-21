using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class PanelSkill : UIBase
    {
        SkillSlot[] m_slotList;

        protected override void Start()
        {
            base.Start();

            m_slotList = GetComponentsInChildren<SkillSlot>();

            GameEvent.instance.EventBindSkill += OnEventBindSkill;
        }

        private void OnDestroy()
        {
            GameEvent.instance.EventBindSkill -= OnEventBindSkill;
        }

        void OnEventBindSkill(SkillData skill)
        {
            SkillSlot slot = GetEmptySlot();
            if(slot == null)
            {
                Debug.LogError("PanelSkill::OnEventBindSkill we dont't have any more slot");
                return;
            }
            slot.BindSkill(skill);
        }

        SkillSlot GetEmptySlot()
        {
            for(int i= 0; i < m_slotList.Length; ++i)
            {
                if(m_slotList[i].IsEmpty())
                {
                    return m_slotList[i];
                }
            }
            return null;
        }
    }
}