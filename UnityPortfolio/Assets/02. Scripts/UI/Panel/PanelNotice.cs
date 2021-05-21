using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCore
{
    public class PanelNotice : UIBase
    {
        struct NoticeInfo
        {
            public string Text;
            public float NoticeTime;
        }

        Image m_backgroundImage;
        Text m_text;

        Animator m_animator;
        Queue<NoticeInfo> m_noticeList = new Queue<NoticeInfo>();
        float m_currentTime;

        protected override void Start()
        {
            base.Start();

            m_backgroundImage = transform.Find("Background").GetComponent<Image>();
            m_text = m_backgroundImage.transform.Find("Text").GetComponent<Text>();
            m_animator = GetComponent<Animator>();

            GameEvent.instance.EventNotice += OnEventNotice;
        }

        private void OnDestroy()
        {
            GameEvent.instance.EventNotice -= OnEventNotice;
        }

        private void Update()
        {
            if (m_noticeList.Count == 0)
                return;

            m_currentTime += Time.deltaTime;

            NoticeInfo currentInfo = m_noticeList.Peek();

            if(m_currentTime >= currentInfo.NoticeTime)
            {
                m_noticeList.Dequeue();
                if(m_noticeList.Count != 0)
                {
                    m_animator.SetTrigger("FadeOnlyText");
                }
                else
                {
                    m_animator.SetTrigger("FadeOut");
                    m_animator.ResetTrigger("FadeOnlyText");
                    m_animator.ResetTrigger("FadeIn");
                }
            }
        }

        public void OnEventNotice(string text,float time, bool ignoreOverlap = true)
        {
            if(ignoreOverlap)
            {
                foreach(NoticeInfo item in m_noticeList)
                {
                    if (item.Text == text)
                        return;
                }
            }

            NoticeInfo info = new NoticeInfo();
            info.Text = text;
            info.NoticeTime = time;

            m_noticeList.Enqueue(info);

            if (m_noticeList.Count == 1)
            {
                m_currentTime = 0f;
                m_text.text = info.Text;
                m_animator.SetTrigger("FadeIn");
            }
        }

        public void OnAnimEventChangeText()
        {
            if (m_noticeList.Count == 0)
                return;
            NoticeInfo currentInfo = m_noticeList.Peek();
            m_text.text = currentInfo.Text;
        }
    }
}