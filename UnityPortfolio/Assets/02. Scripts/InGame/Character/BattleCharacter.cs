using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MyCore
{
    public class BattleCharacter : Character
    {
        static readonly int _animHashDamage = Animator.StringToHash("Damage");

        [Header("BattleCharacter")]
        [SerializeField]protected int m_fullHP;
        [SerializeField]protected int m_hp;

        protected List<SkillBase> m_skillList = new List<SkillBase>();
        protected SkillBase m_currentPlaySkill;
        public int hp { get { return m_hp; } }
        public int fullHP { get { return m_fullHP; } }

        protected override void Update()
        {
            base.Update();

            for(int i =0; i < m_skillList.Count; ++i)
            {
                m_skillList[i].Update();
            }
        }

        public virtual void ApplyDamage(int damage)
        {
            if (m_hp <= 0)
                return;

            m_hp -= damage;
            GameEvent.instance.
                OnEventPlayDamageText(new DamageTextInfo(
                    transform.position + Vector3.up, damage.ToString(), Color.red));
            if(m_hp <= 0)
            {
                OnDead();
            }
            else
            {
                m_animator.SetTrigger(_animHashDamage);
            }
        }

        public virtual void OnDead()
        {

        }

        public virtual void AddSkill(SkillBase skill)
        {
            m_skillList.Add(skill);
        }

        public void OnAnimEventSkillEnd()
        {
            m_currentPlaySkill = null;
            ChangeState(State.Idle);
        }

        public void OnAnimPlayEffect()
        {
            //TODO 추후 스킬 데이터 개선하면 이 부분도 처리
            PlayParticle(m_currentPlaySkill.skillData.FXPrefabPath,
                Vector3.up, 45f);
        }

        protected SkillBase GetPerfectSkill()
        {
            IEnumerable<SkillBase> result = from skill in m_skillList
                                            where skill.isCoolDown == false
                                            orderby skill.skillData.SkillDamage descending
                                            select skill;

            if (result.Count<SkillBase>() == 0)
                return null;

            return result.First<SkillBase>();
        }

        public void PlaySkill(SkillBase skill)
        {
            if (skill == null)
                return;

            MoveEnd();

            m_currentPlaySkill = skill;
            ChangeState(State.Skill);
            m_currentPlaySkill.Play();

        }

        public void PlayParticle(string fxPath,Vector3 offsetLocation,float zRotate)
        {
            GameObject fx = Resources.Load(fxPath) as GameObject;
            Instantiate(fx, transform.position + offsetLocation, 
                Quaternion.Euler(0f, transform.eulerAngles.y, zRotate));
            Debug.Log("PlayerParticle : " + fxPath);
        }

        const int _montageLayer = 1;

        public void PlaySkillAnim(string skillName)
        {
            m_animator.Play(skillName, _montageLayer);
        }
    }
}