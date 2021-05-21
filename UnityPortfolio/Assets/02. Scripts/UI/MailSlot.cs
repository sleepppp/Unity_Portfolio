using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCore
{
    public class MailSlot : MonoBehaviour
    {
        MailInfo m_info;

        Text m_titleText;
        Text m_dateText;

        public MailInfo info { get { return m_info; } }

        public void Init(MailInfo info)
        {
            m_titleText = transform.Find("Title").GetComponent<Text>();
            m_dateText = transform.Find("Date").GetComponent<Text>();

            m_info = info;

            m_titleText.text = m_info.Name;
            m_dateText.text = System.DateTime.Now.ToString("MM-dd");

            transform.Find("Background").GetComponent<Button>().onClick.AddListener(OnButtonUp);
        }

        void OnButtonUp()
        {
            UIView uiView = UINavigation.instance.Push("PopupMailDetail");
            PopupMailDetail detail = uiView as PopupMailDetail;
            detail.Init(m_info);
        }
    }
}