using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace MyCore
{
    public class InteractionHUD : UIBase
    {
        event Action<InteractionHUD> EventInteraction; 

        HUDInfo m_hudInfo;
        Image m_image;
        Text m_text;

        public HUDInfo info { get { return m_hudInfo; } }

        public void Init(HUDInfo info,Action<InteractionHUD>[] arrNotifyEvent)
        {
            m_image = transform.Find("Button").GetComponent<Image>();
            m_text = transform.Find("Text").GetComponent<Text>();

            m_hudInfo = info;

            transform.Find("Button").GetComponent<CustomButton>().EventDown += OnButtonDown;

            for(int i =0; i < arrNotifyEvent.Length; ++i)
            {
                EventInteraction += arrNotifyEvent[i];
            }
        }

        private void LateUpdate()
        {
            Vector3 worldPoint = m_hudInfo.TargetHUDPoint.position;
            Vector3 screenPoint = GameManager.instance.gameMode.mainCamera.WorldToScreenPoint(worldPoint);
            Vector3 finalPoint = TransformScreenPointInCameraCanvas(screenPoint, m_rectTransform);
            m_rectTransform.position = finalPoint;
        }

        void OnButtonDown()
        {
            EventInteraction?.Invoke(this);
        }
    }
}