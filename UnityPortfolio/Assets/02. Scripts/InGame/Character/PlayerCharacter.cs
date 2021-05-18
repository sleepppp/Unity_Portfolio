using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class PlayerCharacter : Character
    {
        static readonly int _animHashIsBattleMode = Animator.StringToHash("IsBattleMode");
        static readonly string _changeToBattleMode = "ChangeToBattleMode";
        static readonly string _changeToNoneBattleMode = "ChangeToNoneBattleMode";

        protected bool m_isBattleMode;

        CharacterEquipment m_equipment;

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

        public void ChangeBattleMode(bool bBattle)
        {
            if (m_isBattleMode == bBattle)
                return;

            if (IsEquippedWeapon() == false)
            {
                GameEvent.instance.OnEventNotice("무기가 없어 배틀모드로 진입할 수 없습니다",2f,true);
                return;
            }

            m_isBattleMode = bBattle;
            m_animator.SetBool(_animHashIsBattleMode, m_isBattleMode);
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

        bool IsEquippedWeapon()
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
    }

}