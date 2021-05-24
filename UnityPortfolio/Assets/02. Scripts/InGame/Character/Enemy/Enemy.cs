using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class Enemy : BattleCharacter
    {
        public override void OnDead()
        {
            base.OnDead();

            GameEvent.instance.OnEventDeadEnemy(this);
        }

        protected override void CreateHUD()
        {
            GameEvent.instance.OnEventCreateHUD(this, m_hudPoint, Color.red);
        }
    }
}