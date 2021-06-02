using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MyCore
{
    //모든 UI의 베이스가 되는 스크립트 입니다.
    public class UIBase : MonoBehaviour
    {
        protected Canvas m_myCanvas;
        protected Camera m_uiCamera;
        protected RectTransform m_rectTransform;

        protected virtual void Start()
        {
            InitComponent();
        }

        public void InitComponent()
        {
            m_myCanvas = transform.FindComponentByParent<Canvas>();
            m_uiCamera = m_myCanvas.worldCamera;
            m_rectTransform = transform as RectTransform;
        }

        //Camera overlay Canvas를 사용할 경우 컴포넌트 들은 해당 함수를 통해 좌표를 변경해서 사용해야 합니다. 

        protected Vector3 ScreenPointToWorldPointInRectangle(Vector2 screenPoint,RectTransform transform)
        {
            Vector3 result;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(transform, screenPoint,
                m_uiCamera, out result);
            return result;
        }

        protected Vector3 WorldPointToScreenPointInCameraCanvas(Vector3 worldPosition,
            RectTransform transform)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
            return ScreenPointToWorldPointInRectangle(screenPoint, transform);
        }
    }
}