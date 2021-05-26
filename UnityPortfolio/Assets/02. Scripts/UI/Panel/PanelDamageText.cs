using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class PanelDamageText : UIBase
    {
        GameObject m_prefabDamageText;

        List<DamageText> m_textList = new List<DamageText>();

        protected override void Start()
        {
            base.Start();

            m_prefabDamageText = Resources.Load("Prefabs/UI/DamageText") as GameObject;

            for(int i =0; i < 5; ++i)
            {
                DamageText text = CreateDamageText();
                text.gameObject.SetActive(false);
                m_textList.Add(text);
            }

            GameEvent.instance.EventPlayDamageText += OnEventPlayDamageText;
        }

        private void OnDestroy()
        {
            GameEvent.instance.EventPlayDamageText -= OnEventPlayDamageText;
        }

        DamageText CreateDamageText()
        {
            GameObject newObject = Instantiate(m_prefabDamageText, transform);
            newObject.gameObject.SetActive(false);
            DamageText text = newObject.GetComponent<DamageText>();
            text.InitComponent();

            text.EventEndDamageText += OnEventEndDamageText;
            m_textList.Add(text);
            return text;
        }


        void OnEventEndDamageText(DamageText text)
        {
            m_textList.Add(text);
        }

        DamageText PopDamageText()
        {
            if (m_textList.Count == 0)
            {
                CreateDamageText();
            }

             DamageText text = m_textList[m_textList.Count - 1];
             m_textList.RemoveAt(m_textList.Count - 1);
            return text;
        }

        void OnEventPlayDamageText(DamageTextInfo info)
        {
            DamageText text = PopDamageText();
            text.StartTexting(info);
        }

    }
}