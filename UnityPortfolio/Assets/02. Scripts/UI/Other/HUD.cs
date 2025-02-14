﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCore
{
    public struct HUDInfo
    {
        public Character TargetCharacter;
        public Transform TargetHUDPoint;
    }

    public class HUD :UIBase
    {
        HUDInfo m_hudInfo;
        Image m_iconImage;
        Text m_nameText;

        public HUDInfo info { get { return m_hudInfo; } set { m_hudInfo = value; } }

        public void Init(HUDInfo info,Color color)
        {
            m_iconImage = transform.Find("Icon").GetComponent<Image>();
            m_nameText = transform.Find("Name").GetComponent<Text>();
            m_hudInfo = info;
            m_nameText.text = m_hudInfo.TargetCharacter.characterName;
            m_nameText.color = color;
        }

        private void LateUpdate()
        {
            Vector3 worldPoint = m_hudInfo.TargetHUDPoint.position;
            Vector3 screenPoint = GameManager.instance.gameMode.mainCamera.WorldToScreenPoint(worldPoint);
            Vector3 finalPoint = ScreenPointToWorldPointInRectangle(screenPoint, m_rectTransform);
            m_rectTransform.position = finalPoint;
        }

    }
}