using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class Rotater : MonoBehaviour
    {
        [SerializeField]float m_rotateSpeed;
        [SerializeField] Vector3 m_rotateAxis;
        
        public float rotateSpeed { get { return m_rotateSpeed; } set { m_rotateSpeed = value; } }
        public Vector3 rotateAxis { get { return m_rotateAxis; } set { m_rotateAxis = value; } }

        private void Update()
        {
            transform.Rotate(m_rotateAxis * m_rotateSpeed * Time.deltaTime);
        }
    }
}