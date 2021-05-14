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
        public void PlayEvent(float duration , Vector3 targetPoint, Quaternion targetRotation,Action notify)
        {
            Action[] arr;
            if (notify != null)
            {
                arr = new Action[2];
                arr[0] = OnEventEndEvent;
                arr[1] = notify;
            }
            else
            {
                arr = new Action[1];
                arr[0] = OnEventEndEvent;
            }

            m_eventCamera.PlayEvent(duration, targetPoint, targetRotation,arr);
            //m_eventCamera.EventEndEvent += OnEventEndEvent;
            m_thirdPersonCamera.isPlay = false;
        }
        // =========================================================================
        public void ReverseEvent(Action notify)
        {
            Action[] arr;
            if (notify != null)
            {
                arr = new Action[2];
                arr[0] = OnEventEndReverseEvent;
                arr[1] = notify;
            }
            else
            {
                arr = new Action[1];
                arr[0] = OnEventEndReverseEvent;
            }
            
           m_eventCamera.ReverseEvent(arr);
            m_thirdPersonCamera.isPlay = false;
        }
        // =========================================================================
        void OnEventEndEvent()
        {
            //m_thirdPersonCamera.isPlay = true;
        }

        // =========================================================================
        void OnEventEndReverseEvent()
        {
            m_thirdPersonCamera.isPlay = true;
        }
    }
}
