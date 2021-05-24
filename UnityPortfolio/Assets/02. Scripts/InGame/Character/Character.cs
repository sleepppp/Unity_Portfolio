using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyCore
{
    [RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent),typeof(Animator))]
    public class Character : MovementObject
    {
        // =========================================================================
        protected static readonly int _animHashMainState = Animator.StringToHash("MainState");
        // =========================================================================
        public enum State : int
        {
            Idle = 0,
            Move = 1,
            Skill = 2
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
        [Header("Character")]
        [SerializeField] protected string m_name;

        protected Transform m_hudPoint;

        public string characterName { get { return m_name; } }
        // =========================================================================
        protected virtual void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_navMeshAgent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();

            m_hudPoint = transform.Find("HUDPoint");

            m_state = State.Idle;
            m_navMeshAgent.speed = m_moveSpeed;
            m_navMeshAgent.enabled = false;

            if (m_hudPoint)
                CreateHUD();
        }
        // =========================================================================
        protected virtual void Update()
        {
            if(IsMoveState())
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
            if (CanMove() == false)
                return;
            ChangeState(State.Move);
            SetMoveType(MoveType.Rigidbody);
            m_rigidbody.MovePosition(m_rigidbody.position + dir * m_moveSpeed * Time.deltaTime);
            Quaternion rotate = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotate, m_rotateSpeed * Time.deltaTime);
        }
        // =========================================================================
        public override void MoveTo(Vector3 targetPoint)
        {
            if (CanMove() == false)
                return;
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
            SetAnimMainState((int)m_state);
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
        bool IsMoveState()
        {
            if (m_state != State.Move)
                return false;

            if (m_moveType == MoveType.None)
                return false;

            return true;
        }

        // =========================================================================
        public void PlaySkill(SkillData skill)
        {
            if (m_state == State.Skill)
                return;
            ChangeState(State.Skill);
            m_animator.Play(skill.MontageName,1);
        }

        // =========================================================================
        protected virtual void CreateHUD()
        {
            GameEvent.instance.OnEventCreateHUD(this, m_hudPoint, Color.white);
        }
    }
}