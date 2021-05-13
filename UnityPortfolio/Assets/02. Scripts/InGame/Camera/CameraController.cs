using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KSW
{
    public class CameraController : MonoBehaviour
    {
        // =========================================================================
        public event Action EventEndEvent;
        // =========================================================================
        ThirdPersonCamera m_thirdPersonCamera;
        EventCamera m_eventCamera;
        // =========================================================================
        private void Start()
        {
            m_thirdPersonCamera = GetComponent<ThirdPersonCamera>();
            m_eventCamera = GetComponent<EventCamera>();

            // {{ Init Camera ~
            CameraScriptableObject cameraStartData = Resources.Load("Camera/StartCamera") as CameraScriptableObject;
            GetComponent<ThirdPersonCamera>().SetValues(cameraStartData);
            // }}
        }
        // =========================================================================
        public void PlayEvent(float duration , Vector3 targetPoint, Quaternion targetRotation)
        {
            m_eventCamera.PlayEvent(duration, targetPoint, targetRotation);
            m_eventCamera.EventEndEvent += OnEventEndEvent;
            m_thirdPersonCamera.isPlay = false;
        }
        // =========================================================================
        public void ReverseEvent()
        {
            m_eventCamera.ReverseEvent();
            m_thirdPersonCamera.isPlay = false;
        }
        // =========================================================================
        void OnEventEndEvent()
        {
            m_thirdPersonCamera.isPlay = true;
        }
    }
}
