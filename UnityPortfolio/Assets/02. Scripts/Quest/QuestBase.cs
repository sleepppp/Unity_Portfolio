using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyCore.Data;

namespace MyCore.Data
{
    public enum QuestType : int
    {
        None = 0,
        killMonster = 1,
        CommunicationWithNPC = 2,
    }
}

namespace MyCore
{
    public abstract class QuestBase
    {
        protected QuestData m_data;
        protected bool m_isFail;
        protected bool m_isSucceeded;

        public QuestData data { get { return m_data; } set { m_data = value; } }
        public bool isFail { get { return m_isFail; } }
        public bool isSucceeded { get { return m_isSucceeded; } }

        public abstract void Start();
        public abstract void Update();
    }

    public class QuestFirstTalkWithRin : QuestBase
    {
        public QuestFirstTalkWithRin() : base()
        {

        }

        public override void Start()
        {
            GameEvent.instance.EventPlayDialog += OnEventPlayDialog;
        }

        public override void Update()
        {
            
        }

        void OnEventPlayDialog(NPC npc)
        {
            m_isSucceeded = true;
            GameEvent.instance.EventPlayDialog -= OnEventPlayDialog;
            GameEvent.instance.OnEventNotice("퀘스트 : " +  m_data.Name + " 성공!", 3f, false);

            //TODO 서버가 없어서 우선 테스트 용으로 처리
            MailInfo mail = new MailInfo();
            mail.Name = "katana";
            mail.Description = "Item Katana";
            mail.RewardItemID = 0;
            Mail.instance.AddMail(mail);
            //GameEvent.instance.OnEventArriveMail(mail);
        }
    }
}