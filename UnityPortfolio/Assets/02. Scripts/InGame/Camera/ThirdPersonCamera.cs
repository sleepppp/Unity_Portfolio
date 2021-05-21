using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyCore
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        // =========================================================================
        [Header("Auto")]
        [SerializeField] Transform m_target;
        [SerializeField] float m_distance;
        [SerializeField] float m_targetHeight;
        [SerializeField] float m_pitchValue;
        [SerializeField] float m_yawValue;
        bool m_isPlay = true;
        float m_stickDragTime = 0f;

        Coroutine m_coroutineInterpoltate;
        // =========================================================================
        public float distance { get { return m_distance; } set { m_distance = value; } }
        public float targetHeight { get { return m_targetHeight; } set { m_targetHeight = value; } }
        public float pitch { get { return m_pitchValue; } set { m_pitchValue = value; } }
        public float yaw { get { return m_yawValue; } set { m_yawValue = value; } }
        public bool isPlay { get { return m_isPlay; } set { m_isPlay = value; } }
        // =========================================================================
        private void LateUpdate()
        {
            if(m_isPlay)
                UpdateAutoCamera();
        }
        // =========================================================================
        private void OnEnable()
        {
            GameEvent.instance.EventScreenScroll += OnEventScreenScroll;
            GameEvent.instance.EventZoomInOut += OnEventZoomInOut;
            GameEvent.instance.EventUpdateStickDrag += OnEventUpdateStickDrag;
            GameEvent.instance.EventEndStickDrag += OnEventEndStickDrag;
            m_coroutineInterpoltate = null;
        }
        // =========================================================================
        private void OnDisable()
        {
            GameEvent.instance.EventScreenScroll -= OnEventScreenScroll;
            GameEvent.instance.EventZoomInOut -= OnEventZoomInOut;
            GameEvent.instance.EventUpdateStickDrag -= OnEventUpdateStickDrag;
            GameEvent.instance.EventEndStickDrag -= OnEventEndStickDrag;
            m_coroutineInterpoltate = null;
        }
        // =========================================================================
        public void UpdateAutoCamera()
        {
            transform.position = CalcCameraPositionByProperty();
            transform.LookAt(CalcLookPoint());
        }
        // =========================================================================
        Vector3 CalcLookPoint()
        {
            return m_target.position + new Vector3(0f, m_targetHeight, 0f);
        }
        // =========================================================================
        Vector3 CalcCameraPositionByProperty()
        {
            Vector3 lookPoint = CalcLookPoint();
            Vector3 cameraLocation = lookPoint -
                Quaternion.Euler(m_pitchValue, m_yawValue, 0f) * Vector3.forward * m_distance;
            return cameraLocation;
        }
        // =========================================================================
        void OnEventScreenScroll(Vector2 moveVector)
        {
            if (m_isPlay == false)
                return;
            m_yawValue += moveVector.x;
            m_pitchValue -= moveVector.y;

            m_pitchValue = Mathf.Clamp(m_pitchValue, 20f, 70f);
        }
        // =========================================================================
        void OnEventZoomInOut(float value)
        {
            if (m_isPlay == false)
                return;
            m_distance += value;
            m_distance = Mathf.Clamp(m_distance, 3.5f, 8f);
        }
        // =========================================================================
        public void SetValues(CameraScriptableObject value)
        {
            m_distance = value.Distance;
            m_targetHeight = value.TargetHeight;
            m_pitchValue = value.PitchValue;
            m_yawValue = value.YawValue;
        }
        // =========================================================================
        public void InterpolateValues(CameraScriptableObject taragetValue, float time, Action notifyEndInterpolate = null)
        {
            if(m_coroutineInterpoltate == null)
            {
                m_coroutineInterpoltate = 
                    StartCoroutine(CoroutineInterpolate(taragetValue, time, notifyEndInterpolate));
            }
        }
        // =========================================================================
        IEnumerator CoroutineInterpolate(CameraScriptableObject targetValue, float time, Action notifyEndInterpolate)
        {
            float startDistance = m_distance;
            float startTargetHeight = m_targetHeight;
            float startPitch = m_pitchValue;
            float startYaw = m_yawValue;
            float currentTime = 0f;
            float ratio = 0f;

            while(ratio < 1f)
            {
                currentTime += Time.deltaTime;
                ratio = currentTime / time;
                m_distance = Mathf.Lerp(startDistance, targetValue.Distance, ratio);
                m_targetHeight = Mathf.Lerp(startTargetHeight, targetValue.TargetHeight, ratio);
                m_pitchValue = Mathf.Lerp(startPitch, targetValue.PitchValue, ratio);
                m_yawValue = Mathf.Lerp(startYaw, targetValue.YawValue, ratio);
              
                yield return null;
            }

            notifyEndInterpolate?.Invoke();

            m_coroutineInterpoltate = null;
        }
        // =========================================================================
        public void InterpolateToDefaultSetting()
        {
            CameraScriptableObject dataObject = Resources.Load("Camera/DefaultCamera") as CameraScriptableObject;
            InterpolateValues(dataObject, 1f);
        }
        // =========================================================================
        void OnEventUpdateStickDrag(Vector2 dir, float value)
        {
            if (m_isPlay == false)
                return;

            m_stickDragTime += Time.deltaTime;
            if(m_stickDragTime >= 0.5f && m_coroutineInterpoltate == null)
            {
                InterpolateToDefaultSetting();
                m_stickDragTime = float.MinValue;
            }
        }
        // =========================================================================
        void OnEventEndStickDrag()
        {
            m_stickDragTime = 0f;
        }
    }
}