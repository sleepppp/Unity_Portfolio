using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MyCore
{
    public class PanelMain : UIBase
    {
        Text m_touchScreenText;

        protected override void Start()
        {
            base.Start();

            m_touchScreenText = transform.Find("TouchScreen").GetComponent<Text>();
            m_touchScreenText.DOColor(new Color(1, 1, 1, 0), 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}