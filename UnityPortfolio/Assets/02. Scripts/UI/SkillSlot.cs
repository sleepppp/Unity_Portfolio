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

        SkillData m_skillData;

        protected override void Start()
        {
            m_iconImage = transform.Find("Background/Icon").GetComponent<Image>();
            m_coolTimeImage = transform.Find("Background/CoolTime").GetComponent<Image>();
            m_rockedImage = transform.Find("Background/Rocked").GetComponent<Image>();
        }

        public bool IsEmpty()
        {
            return m_skillData == null;
        }

        public void BindSkill(SkillData skill)
        {
            m_skillData = skill;
            m_rockedImage.enabled = false;
            m_coolTimeImage.fillAmount = 0f;
            Sprite texture = Resources.Load<Sprite>(skill.SpritePath);
            m_iconImage.sprite = texture;
            m_iconImage.gameObject.SetActive(true);
        }
    }
}