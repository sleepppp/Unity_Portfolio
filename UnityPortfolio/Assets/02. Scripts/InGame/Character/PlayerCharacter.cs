using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MyCore
{
    public class PlayerCharacter : BattleCharacter
    {
        static readonly int _animHashIsBattleMode = Animator.StringToHash("IsBattleMode");
        static readonly string _changeToBattleMode = "ChangeToBattleMode";
        static readonly string _changeToNoneBattleMode = "ChangeToNoneBattleMode";

        protected bool m_isBattleMode;

        CharacterEquipment m_equipment;

        BattleCharacter m_target;

        [SerializeField] float m_detectionRange = 5f;

        public BattleCharacter target { get { return m_target; } }

        protected override void Start()
        {
            base.Start();

            StateBehaviour[] arrBehaviour = m_animator.GetBehaviours<StateBehaviour>();
            for(int i =0; i < arrBehaviour.Length; ++i)
            {
                arrBehaviour[i].EventStateEnter += OnAnimStateEnter;
                arrBehaviour[i].EventStateExit += OnAnimStateExit;
            }

            m_equipment = GetComponent<CharacterEquipment>();
        }

        protected override void Update()
        {
            base.Update();

            if(Input.GetKeyDown(KeyCode.Space))
            {
                PlaySkill(null);
            }
        }

        public void ChangeBattleMode(bool bBattle)
        {
            if (m_isBattleMode == bBattle)
                return;

            if (IsEquippedWeapon() == false)
            {
                return;
            }

            m_isBattleMode = bBattle;
            m_animator.SetBool(_animHashIsBattleMode, m_isBattleMode);

            if(m_isBattleMode)
            {
                SetTarget(FindTarget(true));
            }
            else
            {
                SetTarget(null);
            }
        }

        void OnAnimStateEnter(AnimatorStateInfo stateInfo, int layer)
        {
            if(stateInfo.IsName(_changeToBattleMode) || stateInfo.IsName(_changeToNoneBattleMode))
            {
                SetMovementStatic(true);
            }
        }

        void OnAnimStateExit(AnimatorStateInfo stateInfo, int layer)
        {
            if (stateInfo.IsName(_changeToBattleMode) || stateInfo.IsName(_changeToNoneBattleMode))
            {
                SetMovementStatic(false);
            }
        }

        public bool IsEquippedWeapon()
        {
            //TODO 임시로 Equipment로 검사합니다. Weapon 스크립트 작성 시에 수정 필요 
            if (m_equipment.IsEquipped(m_equipment.backJoint) ||
                m_equipment.IsEquipped(m_equipment.rightHandJoint))
                return true;

            return false;
        }

        // =========================================================================
        public void OnAnimEventChangeJointToRightHand()
        {
            Transform mainWeapon = m_equipment.mainWeapon;
            m_equipment.Equipped(m_equipment.rightHandJoint, mainWeapon, true);
        }

        // =========================================================================
        public void OnAnimEventChangeJointToBack()
        {
            Transform mainWeapon = m_equipment.mainWeapon;
            m_equipment.Equipped(m_equipment.backJoint, mainWeapon, true);
        }

        // =========================================================================
        public void SetTarget(BattleCharacter target)
        {
            if (m_target == target)
                return;
            GameManager.instance.gameMode.SelectTarget(target, m_target);
            m_target = target;
        }

        // =========================================================================
        BattleCharacter FindTarget(bool bNewTarget)
        {
            return FindTarget(GameManager.instance.gameMode.enemyList, bNewTarget);
        }


        // =========================================================================
        BattleCharacter FindTarget(List<Enemy>enemyList, bool bNewTarget)
        {
            IEnumerable<Enemy> result = from target in enemyList
                                        where (bNewTarget ? target != m_target : target)
                                        && Vector3.Distance(transform.position, target.transform.position) < m_detectionRange
                                        && target.hp > 0
                                        orderby Vector3.Distance(transform.position, target.transform.position) ascending
                                        select target;

            if (result.Count<Enemy>() == 0)
                return null;    

            return result.First<Enemy>();
        }

        // =========================================================================
        public void MoveToTarget()
        {
            if (m_target == null)
                return;

            MoveTo(m_target.transform.position);
        }

        public override void AddSkill(SkillBase skill)
        {
            base.AddSkill(skill);

            if (m_skillList.Count < 4)
            {
                GameEvent.instance.OnEventBindSkill(skill);
            }
        }
    }

}