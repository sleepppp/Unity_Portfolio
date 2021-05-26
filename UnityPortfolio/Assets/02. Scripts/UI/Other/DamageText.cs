using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCore
{
    public struct DamageTextInfo
    {
        public Vector3 WorldPosition;
        public string Text;
        public Color Color;

        public DamageTextInfo(Vector3 worldPosition,string str, Color color)
        {
            WorldPosition = worldPosition;
            Text = str;
            Color = color;
        }
    }

    public class DamageText : UIBase
    {
        public event System.Action<DamageText> EventEndDamageText;

        [SerializeField]Text m_text;

        public void OnAnimEndDamagText()
        {
            gameObject.SetActive(false);
            EventEndDamageText?.Invoke(this);
        }

        public void StartTexting(DamageTextInfo info)
        {
            gameObject.SetActive(true);
            m_text.text = info.Text;
            m_text.color = info.Color;

            transform.position = WorldPointToScreenPointInCameraCanvas(info.WorldPosition, transform as RectTransform);
        }
    }
}