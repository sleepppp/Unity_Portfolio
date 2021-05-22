using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCore
{
    public class PopupMailDetail : UIView
    {
        Text m_titleText;
        Text m_description;
        MailInfo m_info;

        public void Init(MailInfo info)
        {
            m_titleText.text = info.Name;
            m_description.text = info.Description;
            m_info = info;
            GameReward.instance.ApplyReward(m_info.RewardItemID);
        }

        public override void Show()
        {
            transform.Find("Background/TopGroup/Button").GetComponent<CustomButton>().EventDown += OnExit;
            m_titleText = transform.Find("Background/TopGroup/Title").GetComponent<Text>();
            m_description = transform.Find("Background/BodyGroup/Text").GetComponent<Text>();
        }

        public override void Hide()
        {
            Mail.instance.RemoveMail(m_info);
        }

        void OnExit()
        {
            UIController.instance.Pop();
        }
    }
}