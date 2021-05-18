using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class MailInfo
    {
        public string Name;
        public string Description;
        public int RewardItemID;
    }

    public class Mail : Singleton<Mail>
    {
        List<MailInfo> m_mailList = new List<MailInfo>();

        public List<MailInfo> mailList { get { return m_mailList; } }

        public void AddMail(MailInfo info)
        {
            GameEvent.instance.OnEventArriveMail(info);
            m_mailList.Add(info);
        }

        public void RemoveMail(MailInfo info)
        {
            for(int i =0; i < m_mailList.Count; ++i)
            {
                if(m_mailList[i] == info)
                {
                    GameEvent.instance.OnEventRemoveMail(info);
                    m_mailList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
