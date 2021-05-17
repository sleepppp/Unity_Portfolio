using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KSW
{
    public class SkillButton : UIBase
    {
        Image m_iconImage;
        Image m_coolTimeImage;
        Image m_rockedImage;

        protected override void Start()
        {
            m_iconImage = transform.Find("Background/Icon").GetComponent<Image>();
            m_coolTimeImage = transform.Find("Background/CoolTime").GetComponent<Image>();
            m_rockedImage = transform.Find("Background/Rocked").GetComponent<Image>();
        }
    }
}