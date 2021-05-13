using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KSW
{
    [RequireComponent(typeof(Rigidbody)/*,typeof(NavMeshAgent)*/,typeof(Animator))]
    public class Character : MovementObject
    {
        // =========================================================================
        protected static readonly int _animHashMainState = Animator.StringToHash("MainState");
        // =========================================================================
        public enum State : int
        {
            Idle = 0,
            Move = 1
        }

        public enum MoveType : int
        {
            None = -1,
            Rigidbody = 0,
            NavMeshAgent = 1
        }
        // =========================================================================
        protected Rigidbody m_rigidbody;
        protected NavMeshAgent m_navMeshAgent;
        protected Animator m_animator;
        protected State m_state;
        protected MoveType m_moveType;
        // =========================================================================
        protected virtual void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_navMeshAgent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();

            m_state = State.Idle;
            m_navMeshAgent.speed = m_moveSpeed;
            m_navMeshAgent.enabled = false;
        }
        // =========================================================================
        protected virtual void Update()
        {
            if(IsMove())
            {
                if(m_moveType == MoveType.NavMeshAgent)
                {
                    float distance = Vector3.Distance(m_navMeshAgent.destination , transform.position);
                    if(distance <= m_navMeshAgent.stoppingDistance)
                    {
                        MoveEnd();
                    }
                }
            }
        }
        // =========================================================================
        public override void Move(Vector3 dir)
        {
            ChangeState(State.Move);
            SetMoveType(MoveType.Rigidbody);
            m_rigidbody.MovePosition(m_rigidbody.position + dir * m_moveSpeed * Time.deltaTime);
            Quaternion rotate = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotate, m_rotateSpeed * Time.deltaTime);
        }
        // =========================================================================
        public override void MoveTo(Vector3 targetPoint)
        {
            ChangeState(State.Move);
            SetMoveType(MoveType.NavMeshAgent);
            m_navMeshAgent.SetDestination(targetPoint);
        }
        // =========================================================================
        public override void MoveEnd()
        {
            base.MoveEnd();

            ChangeState(State.Idle);
            SetMoveType(MoveType.None);
        }
        // =========================================================================
        public virtual void ChangeState(State state)
        {
            if (m_state == state)
                return;

            m_state = state;

            switch(m_state)
            {
                case State.Idle:
                    SetAnimMainState((int)m_state);
                    break;
                case State.Move:
                    SetAnimMainState((int)m_state);
                    break;
            }
        }
        // =========================================================================
        void SetAnimMainState(int state)
        {
            m_animator.SetInteger(_animHashMainState, state);
        }
        // =========================================================================
        void SetMoveType(MoveType type)
        {
            if (m_moveType == type)
                return;

            m_moveType = type;

            switch(m_moveType)
            {
                case MoveType.None:
                   
                    break;
                case MoveType.Rigidbody:
                    if (m_navMeshAgent.enabled == true)
                    {
                        m_navMeshAgent.isStopped = true;
                        m_navMeshAgent.enabled = false;
                    }
                    //m_navMeshAgent.enabled = false;
                    break;
                case MoveType.NavMeshAgent:
                    m_navMeshAgent.enabled = true;
                    m_navMeshAgent.isStopped = false;
                    //m_navMeshAgent.enabled = true;
                    break;
            }
        }
        // =========================================================================
        bool IsMove()
        {
            if (m_state != State.Move)
                return false;

            if (m_moveType == MoveType.None)
                return false;

            return true;
        }
        // =========================================================================
    }
}