using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KSW
{
    public class PanelInteractionHUD : UIBase
    {
        List<InteractionHUD> m_hudList = new List<InteractionHUD>();

        GameObject m_prefabHUD;

        protected override void Start()
        {
            m_prefabHUD = Resources.Load("Prefabs/UI/InteractionHUD") as GameObject;

            GameEvent.instance.EventCreateInteractionHUD += OnEventCreateInteractionHUD;
            GameEvent.instance.EventRemoveInteractionHUD += OnEventRemoveInteractionHUD;
        }

        void OnEventCreateInteractionHUD(Character character,Transform hudPoint,Action<InteractionHUD> nortifyEvent)
        {
            if (IsAlreadyCreate(character))
                return;

            Action<InteractionHUD>[] arrEvent = new Action<InteractionHUD>[2];
            arrEvent[0] = nortifyEvent;
            arrEvent[1] = OnEventRemoveInteractionHUD;

            HUDInfo info = new HUDInfo();
            info.TargetCharacter = character;
            info.TargetHUDPoint = hudPoint;

            GameObject newObject = Instantiate(m_prefabHUD, transform);
            InteractionHUD hud = newObject.GetComponent<InteractionHUD>();
            hud.Init(info, arrEvent);

            m_hudList.Add(hud);
        }

        void OnEventRemoveInteractionHUD(Character character)
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

        void OnEventRemoveInteractionHUD(InteractionHUD hud)
        {
            for (int i = 0; i < m_hudList.Count; ++i)
            {
                if (m_hudList[i] == hud)
                {
                    Destroy(m_hudList[i].gameObject);
                    m_hudList.RemoveAt(i);
                    return;
                }
            }
        }

        bool IsAlreadyCreate(Character character)
        {
            for (int i = 0; i < m_hudList.Count; ++i)
            {
                if (m_hudList[i].info.TargetCharacter == character)
                    return true;
            }

            return false;
        }
    }
}