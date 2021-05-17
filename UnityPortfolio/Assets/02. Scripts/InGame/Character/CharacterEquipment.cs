using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class CharacterEquipment : MonoBehaviour
    {
        [SerializeField] Transform m_backJoint;
        [SerializeField] Transform m_rightHandJoint;
        
        public Transform rightHandJoint { get { return m_rightHandJoint; } }
        public Transform backJoint { get { return m_backJoint; } }

        public bool IsEquipped(Transform joint)
        {
            return joint.childCount != 0;
        }

        public void Equipped(Transform joint, Transform equipment)
        {
            equipment.parent = joint;
            equipment.localPosition = Vector3.zero;
            equipment.localRotation = Quaternion.identity;
        }
    }
}