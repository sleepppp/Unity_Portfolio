using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCore
{
    public class SkillSlot : UIBase
    {
        Image m_iconImage;
        Image m_coolTimeImage;
        Image m_rockedImage;

        SkillBase m_skill;

        protected override void Start()
        {
            m_iconImage = transform.Find("Background/Icon").GetComponent<Image>();
            m_coolTimeImage = transform.Find("Background/CoolTime").GetComponent<Image>();
            m_rockedImage = transform.Find("Background/Rocked").GetComponent<Image>();
        }

        public bool IsEmpty()
        {
            return m_skill == null;
        }

        public void BindSkill(SkillBase skill)
        {
            m_skill = skill;
            m_rockedImage.enabled = false;
            m_coolTimeImage.fillAmount = 0f;
            Sprite texture = Resources.Load<Sprite>(m_skill.skillData.SpritePath);
            m_iconImage.sprite = texture;
            m_iconImage.gameObject.SetActive(true);
        }
    }
}