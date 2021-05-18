using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class PopupMail : UIView
    {
        RectTransform m_contentTransform;
        RectTransform m_targetOutline;

        List<MailSlot> m_slotList = new List<MailSlot>();

        public override void Show()
        {
            m_targetOutline = transform.Find("Mail/TargetMailOutline") as RectTransform;

            CustomButton exitButton = transform.Find("Mail/TopGroup/Button").GetComponent<CustomButton>();
            exitButton.EventUp += OnExit;

            m_contentTransform = transform.Find("Mail/MailGroup/Content") as RectTransform;

            GameObject prefabSlot = Resources.Load("Prefabs/UI/MailSlot") as GameObject;

            List<MailInfo> mailList = Mail.instance.mailList;
            for(int i =0; i < mailList.Count; ++i)
            {
                GameObject newSlotObject = Instantiate(prefabSlot, m_contentTransform);
                MailSlot slot = newSlotObject.GetComponent<MailSlot>();
                slot.Init(mailList[i]);
                m_slotList.Add(slot);
                //if(i == 0)
                //{
                //    m_targetOutline.parent = slot.transform;
                //}
            }

            GameEvent.instance.EventRemoveMail += OnEventRemoveMail;
        }

        public override void Hide()
        {
           
        }

        void OnExit()
        {
            UINavigation.instance.Pop();
        }

        void OnEventRemoveMail(MailInfo info)
        {
            for(int i =0; i < m_slotList.Count; ++i)
            {
                if(m_slotList[i].info == info)
                {
                    Destroy(m_slotList[i].gameObject);
                    m_slotList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}