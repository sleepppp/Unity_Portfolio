using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class RoutineTest : MonoBehaviour
    {
        IEnumerator m_handler;

        private void Start()
        {
            m_handler = UpdateManager.instance.StartRoutine(RoutineLog());
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                UpdateManager.instance.RemoveRoutine(m_handler);
            }
        }

        IEnumerator RoutineLog()
        {
            while(true)
            {
                Debug.Log("Log");
                yield return new WaitForSeconds(1f);
            }

        }
    }
}