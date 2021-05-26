using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace MyCore
{
    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action EventDown;
        public event Action EventStay;
        public event Action EventUp;

        bool m_isDown = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            EventDown?.Invoke();
            m_isDown = true;
        }

        void Update()
        {
            if (m_isDown)
                EventStay?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventUp?.Invoke();
            m_isDown = false;
        }
    }
}