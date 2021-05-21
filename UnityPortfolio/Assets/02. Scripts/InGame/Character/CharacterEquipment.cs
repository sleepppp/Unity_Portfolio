using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class CharacterEquipment : MonoBehaviour
    {
        [SerializeField] Transform m_backJoint;
        [SerializeField] Transform m_rightHandJoint;
        [SerializeField] Transform m_mainWeapon;

        public Transform rightHandJoint { get { return m_rightHandJoint; } }
        public Transform backJoint { get { return m_backJoint; } }
        public Transform mainWeapon { get { return m_mainWeapon; } }

        public bool IsEquipped(Transform joint)
        {
            return joint.childCount != 0;
        }

        public void Equipped(Transform joint, Transform equipment, bool worldPositionStays = false)
        {
            equipment.SetParent(joint,worldPositionStays);
            //equipment.parent = joint;
            equipment.localPosition = Vector3.zero;
            equipment.localRotation = Quaternion.identity;
        }

        public void SetMainWeapon(Transform mainWeapon)
        {
            m_mainWeapon = mainWeapon;
        }
    }
}