using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KSW
{
    //화면 터치 및 드래깅, 줌인 줌아웃은 이곳에서 처리합니다.
    public class PanelTouchScreen : MonoBehaviour, IPointerDownHandler,IDragHandler, IPointerUpHandler
    {
        [SerializeField] float m_touchInterval = 0.5f;
        float m_lastTouchTime;

        Vector2 m_lastInputPosition;

        bool m_isTouched = false;

        float m_lastTouchDistance = 0f;
        int m_lastTouchCount = 0;

        void Update()
        {
            int currentTouchCount = Mathf.Clamp(Input.touchCount, 0, 2);
            if(currentTouchCount != m_lastTouchCount)
            {
               if(currentTouchCount == 2)
               {
                    Touch[] arrTouch = Input.touches;
                    m_lastTouchDistance = Vector3.Distance(arrTouch[0].position, arrTouch[1].position);
                }
               else if(currentTouchCount == 1)
                {
                    m_lastTouchDistance = 0f;
                }
            }

            if(currentTouchCount == 2)
            {
                Touch[] arrTouch = Input.touches;
                float distance = Vector3.Distance(arrTouch[0].position, arrTouch[1].position);
                OnZoomInOut(distance - m_lastTouchDistance);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_lastTouchTime = Time.time;
            m_lastInputPosition = eventData.position;

            m_isTouched = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 movedVector = eventData.position - m_lastInputPosition;
            GameEvent.instance.OnEventScreenScroll(movedVector);
            m_lastInputPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Time.time - m_lastTouchTime < m_touchInterval)
            {
                GameEvent.instance.OnEventTouchWorld(eventData.position);
            }

            m_isTouched = false;
        }

        void OnZoomInOut(float value)
        {
            GameEvent.instance.OnEventZoomInOut(value);
        }
    }
}