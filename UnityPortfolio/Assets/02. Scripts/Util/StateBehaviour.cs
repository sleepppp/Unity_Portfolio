using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KSW
{
    public class StateBehaviour : StateMachineBehaviour
    {
        public event Action<AnimatorStateInfo, int> EventStateEnter;
        public event Action<AnimatorStateInfo, int> EventStateExit;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            EventStateEnter?.Invoke(stateInfo, layerIndex);
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateIK(animator, stateInfo, layerIndex);

            EventStateExit?.Invoke(stateInfo, layerIndex);
        }
    }
}