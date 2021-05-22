using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCore
{
    public class PanelSetting : UIBase
    {
        Image m_menuIconImage;
        Image m_mailIconImage;
        Image m_menuRedDotImage;
        Image m_mailRedDotImage;

        protected override void Start()
        {
            base.Start();

            m_menuIconImage = transform.Find("Menu").GetComponent<Image>();
            m_mailIconImage = transform.Find("Mail").GetComponent<Image>();
            m_menuRedDotImage = transform.Find("Menu/RedDot").GetComponent<Image>();
            m_mailRedDotImage = transform.Find("Mail/RedDot").GetComponent<Image>();

            transform.Find("Mail").GetComponent<CustomButton>().EventDown += OnEventButtonDownMail;

            GameEvent.instance.EventArriveMail += OnEventArriveMail;
            GameEvent.instance.EventPlayDialog += OnEventStartDialog;
            GameEvent.instance.EventEndDialog += OnEventEndDialog;
        }

        private void OnDestroy()
        {
            GameEvent.instance.EventArriveMail -= OnEventArriveMail;
            GameEvent.instance.EventPlayDialog -= OnEventStartDialog;
            GameEvent.instance.EventEndDialog -= OnEventEndDialog;
        }

        void OnEventButtonDownMail()
        {
            m_mailRedDotImage.gameObject.SetActive(false);
            UIController.instance.Push("PopupMail");
        }

        void OnEventArriveMail(MailInfo mailInfo)
        {
            m_mailRedDotImage.gameObject.SetActive(true);
        }

        void OnEventStartDialog(NPC npc)
        {
            m_menuIconImage.gameObject.SetActive(false);
            m_mailIconImage.gameObject.SetActive(false);
        }

        void OnEventEndDialog()
        {
            m_menuIconImage.gameObject.SetActive(true);
            m_mailIconImage.gameObject.SetActive(true);
        }
    }
}