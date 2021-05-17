using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    //static 오브젝트를 제외한 오브젝트는 해당 오브젝트를 상속 받습니다.
    //MovementObject 를 상속받은 오브젝트는 PalyerController로부터 Input을 받을 수 있습니다. 
    public class MovementObject : MonoBehaviour
    {
        // =========================================================================
        [Header("MovementObject")]
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected float m_rotateSpeed;
        protected bool m_movementStatic = false;
    
        PlayerController m_owner;       //나를 조종 중인 컨트롤러
        Coroutine m_coroutineMoveTo;
        // =========================================================================
        public PlayerController owner { get { return m_owner; } }
        // =========================================================================
        public virtual void Move(Vector3 dir)
        {
            if (CanMove() == false)
                return;
            transform.Translate(dir * m_moveSpeed * Time.deltaTime);
        }
        // =========================================================================
        public virtual void MoveTo(Vector3 targetPoint)
        {
            if (CanMove() == false)
                return;

            if (m_coroutineMoveTo != null)
                StopCoroutine(m_coroutineMoveTo);
            m_coroutineMoveTo = StartCoroutine(CoroutineMoveTo(targetPoint));
        }
        // =========================================================================
        public virtual void SetPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }
        // =========================================================================
        public virtual void Rotate(float angle)
        {
            transform.Rotate(0f, angle * m_rotateSpeed * Time.deltaTime, 0f);
        }
        // =========================================================================
        public virtual void MoveEnd()
        {
            if (m_coroutineMoveTo != null)
            {
                StopCoroutine(m_coroutineMoveTo);
                m_coroutineMoveTo = null;
            }
        }
        // =========================================================================
        IEnumerator CoroutineMoveTo(Vector3 targetPoint)
        {
            while(Vector3.Distance(transform.position,targetPoint) > 0.1f)
            {
                Vector3 dir = targetPoint - transform.position;
                transform.Translate(dir.normalized * m_moveSpeed * Time.deltaTime);
                yield return null;
            }
            m_coroutineMoveTo = null;
        }
        // =========================================================================
        public virtual void SetOwner(PlayerController owner)
        {
            m_owner = owner;
        }

        public virtual bool CanMove()
        {
            if (m_movementStatic == false)
                return true;

            return false;
        }
        
        public void SetMovementStatic(bool bStatic)
        {
            m_movementStatic = bStatic;
            Debug.Log("MovementObject::SetMovementStatic : " + bStatic);
        }
    }
}
