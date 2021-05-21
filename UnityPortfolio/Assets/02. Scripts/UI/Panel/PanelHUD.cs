using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class PanelHUD : UIBase
    {
        List<HUD> m_hudList = new List<HUD>();

        GameObject m_prefabHUD;

        protected override void Start()
        {
            base.Start();

            GameEvent.instance.EventCreateHUD += OnEventCreateHUD;
            GameEvent.instance.EventRemoveHUD += OnEventRemoveHUD;
            GameEvent.instance.EventShowHUD += OnEventShowHUD;
            GameEvent.instance.EventHideHUD += OnEventHideHUD;

            m_prefabHUD = Resources.Load("Prefabs/UI/HUD") as GameObject;
        }

        private void OnDestroy()
        {
            GameEvent.instance.EventCreateHUD -= OnEventCreateHUD;
            GameEvent.instance.EventRemoveHUD -= OnEventRemoveHUD;
            GameEvent.instance.EventShowHUD -= OnEventShowHUD;
            GameEvent.instance.EventHideHUD -= OnEventHideHUD;
        }

        void OnEventCreateHUD(Character character,Transform hudPoint)
        {
            if (IsAlreadyCreate(character))
                return;

            HUDInfo info = new HUDInfo();
            info.TargetCharacter = character;
            info.TargetHUDPoint = hudPoint;

            GameObject newObject = Instantiate(m_prefabHUD, transform);
            HUD hud = newObject.GetComponent<HUD>();
            hud.Init(info);

            m_hudList.Add(hud);
        }

        void OnEventRemoveHUD(Character character)
        {
            for(int i =0; i < m_hudList.Count;++i)
            {
                if(m_hudList[i].info.TargetCharacter == character)
                {
                    Destroy(m_hudList[i].gameObject);
                    m_hudList.RemoveAt(i);
                    return;
                }
            }
        }

        bool IsAlreadyCreate(Character character)
        {
            for(int i =0; i < m_hudList.Count; ++i)
            {
                if (m_hudList[i].info.TargetCharacter == character)
                    return true;
            }

            return false;
        }

        void OnEventHideHUD()
        {
            for(int i =0; i < m_hudList.Count; ++i)
            {
                m_hudList[i].gameObject.SetActive(false);
            }
        }

        void OnEventShowHUD()
        {
            for (int i = 0; i < m_hudList.Count; ++i)
            {
                m_hudList[i].gameObject.SetActive(true);
            }
        }
    }
}